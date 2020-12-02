using System;
using UnityEngine;
namespace DWP2
{
    /// <summary>
    /// Class for providing water height and velocity data to WaterObjectManager.
    /// </summary>
    public abstract class WaterDataProvider : MonoBehaviour
    {
        public static WaterDataProvider Instance;

        protected Vector3[] _singlePointArray;
        protected float[] _singleHeightArray;

        public virtual void Awake()
        {
            Debug.Assert(Instance == null, "There is more than one WaterDataProvider in the scene. Only one can be present" +
                                           "as the last initialized will overwrite all the others.");
            
            Instance = this;

            _singleHeightArray = new float[1];
            _singlePointArray = new Vector3[1];
        }

        /// <summary>
        /// Does this water system support water height queries?
        /// </summary>
        /// <returns>True if it does, false if it does not.</returns>
        public abstract bool SupportsWaterHeightQueries();
        
        /// <summary>
        /// Does this water system support water normal queries?
        /// </summary>
        /// <returns>True if it does, false if it does not.</returns>
        public abstract bool SupportsWaterNormalQueries();
        
        /// <summary>
        /// Does this water system support water velocity queries?
        /// </summary>
        /// <returns>True if it does, false if it does not.</returns>
        public abstract bool SupportsWaterFlowQueries();

        /// <summary>
        /// Returns water height at each given point.
        /// </summary>
        /// <param name="points">Position array in world coordinates.</param>
        /// <param name="waterHeights">Water height array in world coordinates. Corresponds to positions.</param>
        public virtual void GetWaterHeights(ref Vector3[] points, ref float[] waterHeights)
        {
            // Do nothing. This will use the initial values of water heights (0).
        }

        /// <summary>
        /// Returns water flow at each given point.
        /// Water flow should be in world coordinates and relative to the world, not the WaterObject itself.
        /// WaterObject velocity and angularVelocity are both accounted for inside WaterTriangleJob.
        /// </summary>
        /// <param name="points">Position array in world coordinates.</param>
        /// <param name="waterFlows">Water flow array in world coordinates. Corresponds to positions.</param>
        public virtual void GetWaterFlows(ref Vector3[] points, ref Vector3[] waterFlows)
        {    
            // Do nothing. This will use the initial values of water velocities (0,0,0).
        }
        
        /// <summary>
        /// Returns water normals at each given point.
        /// </summary>
        /// <param name="points">Position array in world coordinates.</param>
        /// <param name="waterNormals">Water normal array in world coordinates. Corresponds to positions.</param>
        public virtual void GetWaterNormals(ref Vector3[] points, ref Vector3[] waterNormals)
        {    
            // Do nothing. This will use the initial values of water normals (0,0,0).
        }
        
        public void GetWaterHeightsFlowsNormals(ref Vector3[] points, ref float[] waterHeights,
            ref Vector3[] waterFlows, ref Vector3[] waterNormals)
        {
            if (WaterObjectManager.Instance == null)
            {
                Debug.LogError("WaterObjectManager is not present in the scene.");
                return;
            }

            GetWaterHeights(ref points, ref waterHeights);

            if(WaterObjectManager.Instance.simulateWaterFlow && SupportsWaterFlowQueries())
            {
                GetWaterFlows(ref points, ref waterFlows);
            }

            if(WaterObjectManager.Instance.simulateWaterNormals && SupportsWaterNormalQueries())
            {
                GetWaterNormals(ref points, ref waterNormals);
            }
        }

        public virtual float GetWaterHeightSingle(Vector3 point)
        {
            _singlePointArray[0] = point;
            GetWaterHeights(ref _singlePointArray, ref _singleHeightArray);
            return _singleHeightArray[0];
        }
    }
}

