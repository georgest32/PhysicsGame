using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DWP2
{
    /// <summary>
    /// Main class for data processing and triangle job management. Handles all the physics calculations.
    /// </summary>
    [Serializable]
    public class WaterObjectManager : MonoBehaviour
    {
        /// <summary>
        /// Static instance of this object.
        /// </summary>
        public static WaterObjectManager Instance;

        /// <summary>
        /// Should buoyant forces be calculated? When disabled the object will sink, but the hydrodynamic forces
        /// will still affect it.
        /// </summary>
        public bool calculateBuoyancyForces = true;

        /// <summary>
        /// Should hydrodynamic forces be calculated? When disabled the object will still float, but interaction
        /// with water will not depend on the velocity of the object.
        /// </summary>
        public bool calculateDynamicForces = true;

        /// <summary>
        /// Should skin drag be calculated? Skin drag applies drag on the surface when the fluid is traveling over it.
        /// </summary>
        public bool calculateSkinDrag = true;

        /// <summary>
        /// Coefficient by which all the non-buoyancy forces will be multiplied.
        /// </summary>
        [Range(0.5f, 2f)]
        public float dynamicForceCoefficient = 1f;

        /// <summary>
        /// Set to 1 for linear force/velocity ratio or >1 for exponential ratio.
        /// If higher than one forces will increase exponentially with speed.
        /// Use 1 for best performance. Any other value will result in additional Mathf.Pow() call per triangle.
        /// </summary>
        [Range(0.5f, 2f)]
        public float dynamicForcePower = 1f;

        /// <summary>
        /// Density of the fluid the object is in. Affects only buoyancy.
        /// </summary>
        public float fluidDensity = 1030f;

        public bool generateGizmos;

        /// <summary>
        /// Called after WaterObjectManager data is synchronized.
        /// </summary>
        [HideInInspector] public UnityEvent onSynchronize = new UnityEvent();

        /// <summary>
        /// Should water velocities be taken into account when calculating forces?
        /// Should be disabled if the water used does not support velocity/flow map queries
        /// Will be automatically disabled if the water system does not support velocity queries.
        /// to save on performance.
        /// </summary>
        public bool simulateWaterFlow = true;

        /// <summary>
        /// Should water normals be taken into account when calculating forces?
        /// Should be disabled if the water if flat or the water system does not support water normal queries
        /// to save on performance.
        /// Will be automatically disabled if the water system does not support normal queries.
        /// </summary>
        public bool simulateWaterNormals = true;

        /// <summary>
        /// Resistant force exerted on an object moving in a fluid. Skin friction drag is caused by the viscosity of fluids and is developed as a fluid moves on the surface of an object.
        /// </summary>
        [Range(0f, 0.2f)]
        public float skinFrictionDrag = 0.01f;

        /// <summary>
        /// Set to 1 for linear dot/force ratio or other than 1 for exponential ratio.
        /// When force is calculated it is multiplied by the dot value between normal of the surface and the velocity.
        /// High power values will result in shallow angles between the two having less of an effect on the final force, and vice versa.
        /// Use 1 for best performance. Any other value will result in additional Mathf.Pow() call per triangle.
        /// </summary>
        [Range(0.5f, 2f)]
        public float velocityDotPower = 1f;

        /// <summary>
        /// When true even inactive objects will get synchronized, but will not be simulated until they become active.
        /// </summary>
        public bool includeInactive = true;

        private int _triCount;
        private int _vertCount;
        private float[] _triAreas;
        private float[] _triDistToSurface;
        private bool _finished;
        private Vector3[] _triForcePoints;
        private bool[] _hasBeenDestroyed;
        private bool _initialized;
        private int[] _triangles;
        private Vector3[] _vertices;
        private Transform[] _vertTransforms;
        private Matrix4x4[] _vertLocalToWorldMatrices;
        private Vector3[] _triNormals;
        private Vector3[] _vertAngVels;
        private Vector3[] _vertRbComs;
        private Vector3[] _vertLinVels;
        private Vector3[] _triForces;
        private bool _scheduled;
        private byte[] _triStates;
        private Vector3[] _triVelocities;
        private Vector3[] _waterFlows;
        private float[] _waterHeights;
        private JobHandle _waterJobHandle;
        private Vector3[] _waterNormals;
        private List<WaterObject> _waterObjects;
        private WaterObject[] _targetWaterObjects;
        private WaterTriangleJob _waterTriJob;
        private Vector3[] _worldVerts;
        public List<WaterObject> WaterObjects => _waterObjects;

        /// <summary>
        /// An array representing the state of each triangle:
        /// 0 - all three verts are under water
        /// 1 - one or two verts are under water
        /// 2 - triangle is above water
        /// 3 - triangle is disabled
        /// 4 - triangle has been deleted
        /// To see if the triangle should be simulated check if its state is less or equal to 2.
        /// For triangle to be touching water its state should be 0 or 1.
        /// </summary>
        public byte[] States
        {
            get => _triStates;
            private set => _triStates = value;
        }

        /// <summary>
        /// Has the WaterObject been destroyed? 
        /// If the object is destroyed but the scene is not synchronized the WaterObject data will still be kept,
        /// but ignored, to prevent reallocating native arrays.
        /// </summary>
        public bool[] IsDestroyed
        {
            get => _hasBeenDestroyed;
            set => _hasBeenDestroyed = value;
        }

        /// <summary>
        /// Forces in world coordinates. Each force should be applied at the corresponding force point.
        /// </summary>
        public Vector3[] Forces
        {
            get => _triForces;
            private set => _triForces = value;
        }

        /// <summary>
        /// Force application points in world coordinates.
        /// </summary>
        public Vector3[] ForcePoints
        {
            get => _triForcePoints;
            private set => _triForcePoints = value;
        }

        /// <summary>
        /// Water heights at each vertex.
        /// </summary>
        public float[] WaterHeights => _waterHeights;

        /// <summary>
        /// Water surface normals at each vertex.
        /// </summary>
        public Vector3[] WaterNormals => _waterNormals;

        /// <summary>
        /// Water surface flow at each vertex.
        /// </summary>
        public Vector3[] WaterFlows => _waterFlows;

        /// <summary>
        /// Triangle normals. Normals are calculated from vertex data. 
        /// Mesh normals are ignored.
        /// </summary>
        public Vector3[] Normals
        {
            get => _triNormals;
            private set => _triNormals = value;
        }

        /// <summary>
        /// Areas of each simulated triangle.
        /// </summary>
        public float[] Areas
        {
            get => _triAreas;
            private set => _triAreas = value;
        }

        /// <summary>
        /// Velocities of centers of each simulated triangle.
        /// </summary>
        public Vector3[] Velocities
        {
            get => _triVelocities;
            private set => _triVelocities = value;
        }

        /// <summary>
        /// Distances to surface from the center of each triangle.
        /// </summary>
        public float[] DistancesToSurface
        {
            get => _triDistToSurface;
            private set => _triDistToSurface = value;
        }

        public Vector3[] P0S { get; private set; }

        public Vector3[] P1S { get; private set; }

        /// <summary>
        /// Total triangle count.
        /// To get the simulated triangle count use ActiveTriCount instead.
        /// </summary>
        public int TriangleCount => _triCount;

        /// <summary>
        /// Total simulated vertex count.
        /// </summary>
        public int VertexCount => _vertCount;

        /// <summary>
        /// Count of currently simulated triangles.
        /// </summary>
        public int ActiveTriCount => States?.Count(s => s <= 2) ?? 0;

        /// <summary>
        /// Count of currently simulated triangles that are under water.
        /// </summary>
        public int ActiveUnderwaterTriCount => States?.Count(s => s <= 1) ?? 0;

        /// <summary>
        /// Count of currently simulated triangles that are above water.
        /// </summary>
        public int ActiveAboveWaterTriCount => States?.Count(s => s == 2) ?? 0;

        /// <summary>
        /// Disabled triangle count.
        /// Triangle will be marked as disabled when the parent GameObject is disabled.
        /// </summary>
        public int DisabledTriCount => States?.Count(s => s == 3) ?? 0;

        /// <summary>
        /// Destroyed triangle count.
        /// Triangle will be marked as destroyed when the parent GameObject is destroyed.
        /// </summary>
        public int DestroyedTriCount => States?.Count(s => s == 4) ?? 0;

        /// <summary>
        /// Total number of disabled and destoryed triangles.
        /// </summary>
        public int InactiveTriCount => States?.Count(s => s == 3 || s == 4) ?? 0;

        private void Initialize()
        {
            if(_initialized) Deallocate();

            // Get all WaterObjects
            _waterObjects = new List<WaterObject>();
            FindSceneWaterObjects(ref _waterObjects, includeInactive);

            // Initialize WaterObjects
            foreach (WaterObject wo in _waterObjects)
            {
                wo.Initialize();
            }
            
            _waterObjects = _waterObjects.Where(w => w.Initialized).ToList();

            // Don't run if nothing to simulate
            int waterObjectCount = _waterObjects.Count;
            if (waterObjectCount == 0)
            {
                return;
            }

            // Initialize arrays
            _hasBeenDestroyed = new bool[waterObjectCount];
            for (int i = 0; i < waterObjectCount; i++)
            {
                _waterObjects[i].WoArrayIndex = i;
                _hasBeenDestroyed[i] = false;
            }

            // Allocate arrays
            int totalTriIndexCount = 0;
            int totalVertCount = 0;
            foreach (WaterObject waterObject in _waterObjects)
            {
                totalTriIndexCount += waterObject.SerializedSimulationMesh.triangles.Length;
                totalVertCount += waterObject.SerializedSimulationMesh.vertices.Length;
            }

            _vertTransforms = new Transform[totalVertCount];
            _triangles = new int[totalTriIndexCount];
            _vertices = new Vector3[totalVertCount];
            _targetWaterObjects = new WaterObject[totalTriIndexCount / 3];
            
            // Get triangle data     
            totalTriIndexCount = 0;
            totalVertCount = 0;
            foreach (WaterObject waterObject in _waterObjects)
            {
                int triIndexCount = waterObject.SerializedSimulationMesh.triangles.Length;
                int vertCount = waterObject.SerializedSimulationMesh.vertices.Length;
                
                waterObject.TriDataStart = totalTriIndexCount / 3;
                waterObject.TriDataLength = triIndexCount / 3;
                waterObject.VertDataStart = totalVertCount;
                waterObject.VertDataLength = vertCount;

                for(int i = 0; i < triIndexCount; i += 3)
                {
                    _triangles[totalTriIndexCount + i] = waterObject.SerializedSimulationMesh.triangles[i] + totalVertCount;
                    _triangles[totalTriIndexCount + i + 1] = waterObject.SerializedSimulationMesh.triangles[i + 1] + totalVertCount;
                    _triangles[totalTriIndexCount + i + 2] = waterObject.SerializedSimulationMesh.triangles[i + 2] + totalVertCount;
                    _targetWaterObjects[totalTriIndexCount / 3 + i / 3] = waterObject;
                }

                for (int i = 0; i < vertCount; i++)
                {
                    _vertices[totalVertCount + i] = waterObject.SerializedSimulationMesh.vertices[i];
                    _vertTransforms[totalVertCount + i] = waterObject.transform;
                }

                if (triIndexCount > 400)
                {
                    Debug.LogWarning($"Excessive number of triangles found on {waterObject.name}'s simulation mesh. " +
                                     $"Such high number of triangles ({triIndexCount / 3}) will not improve simulation quality but will have a " +
                                     $"large performance impact. Use 'Simplify Mesh' option on this WaterObject.");
                }
                
                totalTriIndexCount += triIndexCount;
                totalVertCount += vertCount;
            }

            // Allocate native arrays for water tri job
            _triCount = totalTriIndexCount / 3;
            _vertCount = totalVertCount;
            _waterTriJob.States = new NativeArray<byte>(_triCount, Allocator.Persistent);
            _waterTriJob.ResultForces = new NativeArray<Vector3>(_triCount, Allocator.Persistent);
            _waterTriJob.ResultPoints = new NativeArray<Vector3>(_triCount, Allocator.Persistent);
            _waterTriJob.Vertices = new NativeArray<Vector3>(_vertCount, Allocator.Persistent);
            _waterTriJob.Triangles = new NativeArray<int>(_triCount * 3, Allocator.Persistent);
            _waterTriJob.WaterHeights = new NativeArray<float>(_vertCount, Allocator.Persistent);
            _waterTriJob.WaterFlows = new NativeArray<Vector3>(_vertCount, Allocator.Persistent);
            _waterTriJob.WaterNormals = new NativeArray<Vector3>(_vertCount, Allocator.Persistent);
            _waterTriJob.Velocities = new NativeArray<Vector3>(_triCount, Allocator.Persistent);
            _waterTriJob.Normals = new NativeArray<Vector3>(_triCount, Allocator.Persistent);
            _waterTriJob.Areas = new NativeArray<float>(_triCount, Allocator.Persistent);
            _waterTriJob.RigidbodyCOMs = new NativeArray<Vector3>(_vertCount, Allocator.Persistent);
            _waterTriJob.RigidbodyAngularVels = new NativeArray<Vector3>(_vertCount, Allocator.Persistent);
            _waterTriJob.RigidbodyLinearVels = new NativeArray<Vector3>(_vertCount, Allocator.Persistent);
            _waterTriJob.P0s = new NativeArray<Vector3>(3 * _triCount, Allocator.Persistent);
            _waterTriJob.P1s = new NativeArray<Vector3>(3 * _triCount, Allocator.Persistent);
            _waterTriJob.Distances = new NativeArray<float>(_triCount, Allocator.Persistent);

            // Allocate managed arrays
            // Input
            _waterHeights = new float[_vertCount];
            _waterHeights.Fill(0);
            
            _waterFlows = new Vector3[_vertCount];
            Vector3 zeroVector = Vector3.zero; 
            _waterFlows.Fill(zeroVector);
            
            _waterNormals = new Vector3[_vertCount];
            Vector3 upVector = -Physics.gravity.normalized;
            _waterNormals.Fill(upVector);
            
            _vertLocalToWorldMatrices = new Matrix4x4[_vertCount];
            _vertRbComs = new Vector3[_vertCount];
            _vertLinVels = new Vector3[_vertCount];
            _vertAngVels = new Vector3[_vertCount];

            // Output
            _worldVerts = new Vector3[_vertCount];
            _triStates = new byte[_triCount];
            _triVelocities = new Vector3[_triCount];
            _triForces = new Vector3[_triCount];
            _triForcePoints = new Vector3[_triCount];
            _triAreas = new float[_triCount];
            _triNormals = new Vector3[_triCount];
            _triDistToSurface = new float[_triCount];
            P0S = new Vector3[3 * _triCount];
            P1S = new Vector3[3 * _triCount];

            // Copy data to native arrays
            _waterTriJob.Triangles.FastCopyFrom(_triangles);
            
            // Copy default data
            _waterTriJob.WaterHeights.FastCopyFrom(_waterHeights);
            _waterTriJob.WaterFlows.FastCopyFrom(_waterFlows);
            _waterTriJob.WaterNormals.FastCopyFrom(_waterNormals);

            _initialized = true;
        }

        private void Awake()
        {
            // Check if there is only one WaterObjectManager
            if (FindObjectsOfType<WaterObjectManager>().Length > 1)
            {
                UnityEngine.Debug.LogError("There can be only one WaterObjectManager in the scene.");
            }
            Instance = this;

            _finished = true;
            _scheduled = false;
            _initialized = false;
        }

        private void Start()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            Finish();
            Process();
            Schedule();
        }

        private void OnDisable()
        {
            Deallocate();
        }

        private void OnDrawGizmos()
        {
            if(!_initialized || !generateGizmos) return;

            if (Application.isPlaying)
            {
                Vector3 p00, p01, p02, p10, p11, p12, center;

                for (int i = 0; i < _triCount * 3; i+=3)
                {
                    int iDiv3 = i / 3;
                    //if(_triStates[iDiv3] >= 2) continue;

                    Gizmos.color = Color.Lerp(Color.white, Color.red, (Forces[iDiv3].magnitude * 0.00005f) / Areas[iDiv3]);
                    
                    p00 = P0S[i];
                    p01 = P0S[i + 1];
                    p02 = P0S[i + 2];
                    center = ForcePoints[iDiv3];
                    if (p00 != Vector3.zero && p01 != Vector3.zero && p02 != Vector3.zero)
                    {
                        Gizmos.DrawLine(p00, p01);
                        Gizmos.DrawLine(p01, p02);
                        Gizmos.DrawLine(p02, p00);
                        Gizmos.DrawSphere(center, 0.01f);
                        Gizmos.DrawLine(center, center + Normals[iDiv3] * 0.1f);
                    }
                    
                    p10 = P1S[i];
                    p11 = P1S[i + 1];
                    p12 = P1S[i + 2];
                    if (p10 != Vector3.zero && p11 != Vector3.zero && p12 != Vector3.zero)
                    {
                        Gizmos.DrawLine(p10, p11);
                        Gizmos.DrawLine(p11, p12);
                        Gizmos.DrawLine(p12, p10);
                        Gizmos.DrawSphere(center, 0.01f);
                        Gizmos.DrawLine(center, center + Normals[iDiv3] * 0.1f);
                    }
                    
                    // Visualize distance to water
                    Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
                    Gizmos.DrawLine(center, center + Vector3.up * DistancesToSurface[iDiv3]);
                }
            }
        }


        /// <summary>
        /// Slow. Avoid if at all possible.
        /// Instead of spawning new objects it is preferable to spawn them at start and then disable them - this way
        /// job arrays will get initialized to correct size and yet inactive objects will not be simulated.
        /// </summary>
        public void Synchronize()
        {
            Initialize();
            onSynchronize.Invoke();
        }


        private void OnValidate()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Deallocate();
        }

        private void FindSceneWaterObjects(ref List<WaterObject> waterObjects, bool includeInactive)
        {
            waterObjects =  SceneManager.GetActiveScene().GetRootGameObjects()
                    .SelectMany(g => g.GetComponentsInChildren<WaterObject>(includeInactive))
                    .Where(w => w.gameObject.activeInHierarchy || (!w.gameObject.activeInHierarchy && includeInactive))
                    .ToList();
        }
        
        private void Schedule()
        {
            if (!_initialized || !_finished || _triCount == 0) return;

            if (WaterDataProvider.Instance == null)
            {
                Debug.LogError("There is no WaterDataProvider present in the scene. Since DWP2 2.2 there has to be exactly one WaterDataProvider present in the scene." +
                               "If using flat water attach FlatWaterDataProvider to it.");
                return;
            }
            
            _finished = false;
            _scheduled = true;
            
            //  Fill in new data into managed containers
            _waterTriJob.States.FastCopyTo(_triStates);
            int waterObjectCount = _waterObjects.Count;
            for (int i = 0; i < waterObjectCount; i++)
            {
                WaterObject wo = _waterObjects[i];
                int triDataStart = wo.TriDataStart;
                int triDataEnd = wo.TriDataEnd;
                int vertDataStart = wo.VertDataStart;
                int vertDataEnd = wo.VertDataEnd;

                if (_hasBeenDestroyed[i])
                {
                    for (int j = triDataStart; j < triDataEnd; j++)
                    {
                        _triStates[j] = 4;
                    }

                    continue;
                }

                // Fill in data
                byte state = wo.isActiveAndEnabled ? (byte) 2 : (byte) 3;
                Matrix4x4 cachedLocalToWorldMatrix = wo.transform.localToWorldMatrix;
                Vector3 cachedCenterOfMass = wo.TargetRigidbody.worldCenterOfMass;
                Vector3 cachedLinearVelocity = wo.TargetRigidbody.velocity;
                Vector3 cachedAngularVelocity = wo.TargetRigidbody.angularVelocity;
                
                for (int j = triDataStart; j < triDataEnd; j += 3)
                {
                    _triStates[j/3] = state;
                }

                for (int j = vertDataStart; j < vertDataEnd; j++)
                {
                    _vertLocalToWorldMatrices[j] = cachedLocalToWorldMatrix;
                    _vertRbComs[j] = cachedCenterOfMass;
                    _vertLinVels[j] = cachedLinearVelocity;
                    _vertAngVels[j] = cachedAngularVelocity;
                }
            }

            // Convert local vertex positions to world positions
            for (int i = 0; i < _vertCount; i++)
            {
                _worldVerts[i] = _vertLocalToWorldMatrices[i].MultiplyPoint3x4(_vertices[i]);
            }
            
            // Get water data
            WaterDataProvider.Instance.GetWaterHeightsFlowsNormals(ref _worldVerts, 
                ref _waterHeights, ref _waterFlows, ref _waterNormals);
            
            // Set simulation settings
            _waterTriJob.GlobalUpVector = Vector3.up;
            _waterTriJob.ZeroVector = Vector3.zero;
            _waterTriJob.SimulateWaterNormals = simulateWaterNormals && WaterDataProvider.Instance.SupportsWaterNormalQueries();
            _waterTriJob.SimulateWaterFlow = simulateWaterFlow && WaterDataProvider.Instance.SupportsWaterFlowQueries();
            _waterTriJob.CalculateDynamicForces = calculateDynamicForces;
            _waterTriJob.CalculateBuoyantForces = calculateBuoyancyForces;
            _waterTriJob.CalculateSkinDrag = calculateSkinDrag;
            _waterTriJob.Gravity = Physics.gravity;
            _waterTriJob.FluidDensity = fluidDensity;
            _waterTriJob.DynamicForceFactor = dynamicForceCoefficient;
            _waterTriJob.DynamicForcePower = dynamicForcePower;
            _waterTriJob.VelocityDotPower = velocityDotPower;
            _waterTriJob.SkinDrag = skinFrictionDrag;
            
            // Copy new data to native containers
            _waterTriJob.Vertices.FastCopyFrom(_worldVerts);
            _waterTriJob.WaterHeights.FastCopyFrom(_waterHeights);
            _waterTriJob.WaterFlows.FastCopyFrom(_waterFlows);
            _waterTriJob.WaterNormals.FastCopyFrom(_waterNormals);
            _waterTriJob.States.FastCopyFrom(_triStates);
            _waterTriJob.Velocities.FastCopyFrom(_triVelocities);
            _waterTriJob.RigidbodyCOMs.FastCopyFrom(_vertRbComs);
            _waterTriJob.RigidbodyLinearVels.FastCopyFrom(_vertLinVels);
            _waterTriJob.RigidbodyAngularVels.FastCopyFrom(_vertAngVels);

            _waterJobHandle = _waterTriJob.Schedule(_triCount, 32);
        }

        /// <summary>
        /// Manipulate data retrieved from job before job is started again.
        /// Accessing job data other than here will result in error due to job being unfinished.
        /// </summary>
        private void Process()
        {
            if (!_initialized || !_finished) return;
            
            if (_triCount == 0) return;
            
            // Copy native arrays to managed arrays for faster access
            _waterTriJob.States.FastCopyTo(_triStates);
            _waterTriJob.ResultForces.FastCopyTo(_triForces);
            _waterTriJob.ResultPoints.FastCopyTo(_triForcePoints);
            _waterTriJob.Normals.FastCopyTo(_triNormals);
            _waterTriJob.Areas.FastCopyTo(_triAreas);
            _waterTriJob.Velocities.FastCopyTo(_triVelocities);
            _waterTriJob.Distances.FastCopyTo(_triDistToSurface);
            
            _waterTriJob.P0s.FastCopyTo(P0S);
            _waterTriJob.P1s.FastCopyTo(P1S);

            // Apply forces
            bool initAutoSync = Physics.autoSyncTransforms;
            Physics.autoSyncTransforms = false;
            for (int i = 0; i < _triCount; i++)
            {
                if(_triStates[i] >= 2) continue;
                _targetWaterObjects[i].TargetRigidbody.AddForceAtPosition(_triForces[i], _triForcePoints[i]);
            }
            Physics.autoSyncTransforms = initAutoSync;
        }

        private void Finish()
        {
            if (!_initialized || !_scheduled) return;
            
            _scheduled = false;
            _waterJobHandle.Complete();
            _finished = true;
        }

        private static void FastCopy<T>(T[] source, T[] destination) where T : struct
        {
            Array.Copy(source, destination, source.Length);
        }

        private void Deallocate()
        {
            if (!_initialized) return;
            _initialized = false;

            try
            {
                _waterJobHandle.Complete();
                _waterTriJob.States.Dispose();
                _waterTriJob.ResultForces.Dispose();
                _waterTriJob.ResultPoints.Dispose();
                _waterTriJob.Vertices.Dispose();
                _waterTriJob.Triangles.Dispose();
                _waterTriJob.WaterHeights.Dispose();
                _waterTriJob.WaterFlows.Dispose();
                _waterTriJob.WaterNormals.Dispose();
                _waterTriJob.Velocities.Dispose();
                _waterTriJob.Normals.Dispose();
                _waterTriJob.Areas.Dispose();
                _waterTriJob.RigidbodyCOMs.Dispose();
                _waterTriJob.RigidbodyAngularVels.Dispose();
                _waterTriJob.RigidbodyLinearVels.Dispose();
                _waterTriJob.P0s.Dispose();
                _waterTriJob.P1s.Dispose();
                _waterTriJob.Distances.Dispose();
            }
            catch
            {
                // Possibly a Unity bug where, despite calling Complete(), the job might not be finished.
            }
        }

        /// <summary>
        /// Returns true if point is in water. Works with wavy water too.
        /// </summary>
        public bool PointInWater(Vector3 worldPoint)
        {
            return GetWaterHeight(worldPoint) > worldPoint.y;
        }

        public float GetWaterHeight(Vector3 worldPoint)
        {
            return WaterDataProvider.Instance.GetWaterHeightSingle(worldPoint);
        }
    }
}