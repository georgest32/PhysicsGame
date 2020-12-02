using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace DWP2
{
    /// <summary>
    /// Job for calculating WaterObject forces.
    /// </summary>
    
#if DWP_BURST
    [BurstCompile]
#endif
    public struct WaterTriangleJob : IJobParallelFor
    {
        [ReadOnly] public Vector3 ZeroVector;
        [ReadOnly] public Vector3 GlobalUpVector;

        [ReadOnly] public bool CalculateBuoyantForces;
        [ReadOnly] public bool CalculateDynamicForces;
        [ReadOnly] public bool CalculateSkinDrag;
        
        [ReadOnly] public bool SimulateWaterNormals;
        [ReadOnly] public bool SimulateWaterFlow;

        [ReadOnly] public Vector3 Gravity;
        [ReadOnly] public float FluidDensity;
        [ReadOnly] public float DynamicForceFactor;
        [ReadOnly] public float DynamicForcePower;
        [ReadOnly] public float SkinDrag;
        [ReadOnly] public float VelocityDotPower;
        
        [ReadOnly] public NativeArray<float> WaterHeights;
        [ReadOnly] public NativeArray<Vector3> WaterFlows;
        [ReadOnly] public NativeArray<Vector3> WaterNormals;
        
        [NativeDisableParallelForRestriction] [ReadOnly] public NativeArray<Vector3> Vertices;
        [NativeDisableParallelForRestriction] [ReadOnly] public NativeArray<int> Triangles;
        [ReadOnly] public NativeArray<Vector3> RigidbodyCOMs;
        [ReadOnly] public NativeArray<Vector3> RigidbodyLinearVels;
        [ReadOnly] public NativeArray<Vector3> RigidbodyAngularVels;

        [NativeDisableParallelForRestriction] public NativeArray<Vector3> P0s;
        [NativeDisableParallelForRestriction] public NativeArray<Vector3> P1s;

        /// <summary>
        /// 0 - Under water
        /// 1 - At surface
        /// 2 - Above water
        /// 3 - Disabled
        /// 4 - Destroyed
        /// </summary>
        public NativeArray<byte> States;
        public NativeArray<Vector3> ResultForces;
        public NativeArray<Vector3> ResultPoints;

        public NativeArray<float> Areas;
        public NativeArray<Vector3> Normals;
        public NativeArray<Vector3> Velocities;

        public NativeArray<float> Distances;

        private int _vi0;
        private int _vi1;
        private int _vi2;

        public void Execute(int i)
        {
            if(States[i] >= 3) return;

            _vi0 = Triangles[i * 3];
            _vi1 = Triangles[i * 3 + 1];
            _vi2 = Triangles[i * 3 + 2];

            Vector3 P0 = Vertices[_vi0];
            Vector3 P1 = Vertices[_vi1];
            Vector3 P2 = Vertices[_vi2];

            float wY0 = WaterHeights[_vi0];
            float wY1 = WaterHeights[_vi1];
            float wY2 = WaterHeights[_vi2];
            float y0 = P0.y - wY0;
            float y1 = P1.y - wY1;
            float y2 = P2.y - wY2;

            // All vertices are above water
            if (y0 >= 0 && y1 >= 0 && y2 >= 0)
            {
                P0s[_vi0] = ZeroVector;
                P0s[_vi1] = ZeroVector;
                P0s[_vi2] = ZeroVector;

                P1s[_vi0] = ZeroVector;
                P1s[_vi1] = ZeroVector;
                P1s[_vi2] = ZeroVector;
                
                ResultPoints[i] = ZeroVector;
                ResultForces[i] = ZeroVector;

                States[i] = 2;

                return;
            }
            
            // All vertices are underwater
            if (y0 <= 0 && y1 <= 0 && y2 <= 0)
            {
                ThreeUnderWater(P0, P1, P2, y0, y1, y2, 0, 1, 2, i);
            }
            // 1 or 2 vertices are below the water
            else
            {
                // v0 > v1
                if (y0 > y1)
                {
                    // v0 > v2
                    if (y0 > y2)
                    {
                        // v1 > v2                  
                        if (y1 > y2)
                        {
                            if (y0 > 0 && y1 < 0 && y2 < 0)
                            {
                                // 0 1 2
                                TwoUnderWater(P0, P1, P2, y0, y1, y2, 0, 1, 2, i);
                            }
                            else if (y0 > 0 && y1 > 0 && y2 < 0)
                            {
                                // 0 1 2
                                OneUnderWater(P0, P1, P2, y0, y1, y2, 0, 1, 2, i);
                            }
                        }
                        // v2 > v1
                        else
                        {
                            if (y0 > 0 && y2 < 0 && y1 < 0)
                            {
                                // 0 2 1
                                TwoUnderWater(P0, P2, P1, y0, y2, y1, 0, 2, 1, i);
                            }
                            else if (y0 > 0 && y2 > 0 && y1 < 0)
                            {
                                // 0 2 1
                                OneUnderWater(P0, P2, P1, y0, y2, y1, 0, 2, 1, i);
                            }
                        }
                    }
                    // v2 > v0
                    else
                    {
                        if (y2 > 0 && y0 < 0 && y1 < 0)
                        {
                            // 2 0 1
                            TwoUnderWater(P2, P0, P1, y2, y0, y1, 2, 0, 1, i);
                        }
                        else if (y2 > 0 && y0 > 0 && y1 < 0)
                        {
                            // 2 0 1
                            OneUnderWater(P2, P0, P1, y2, y0, y1, 2, 0, 1, i);
                        }
                    }
                }
                // v0 < v1
                else
                {
                    // v0 < v2
                    if (y0 < y2)
                    {
                        // v1 < v2
                        if (y1 < y2)
                        {
                            if (y2 > 0 && y1 < 0 && y0 < 0)
                            {
                                // 2 1 0
                                TwoUnderWater(P2, P1, P0, y2, y1, y0, 2, 1, 0, i);
                            }
                            else if (y2 > 0 && y1 > 0 && y0 < 0)
                            {
                                // 2 1 0
                                OneUnderWater(P2, P1, P0, y2, y1, y0, 2, 1, 0, i);
                            }
                        }
                        // v2 < v1
                        else
                        {
                            if (y1 > 0 && y2 < 0 && y0 < 0)
                            {
                                // 1 2 0
                                TwoUnderWater(P1, P2, P0, y1, y2, y0, 1, 2, 0, i);
                            }
                            else if (y1 > 0 && y2 > 0 && y0 < 0)
                            {
                                // 1 2 0
                                OneUnderWater(P1, P2, P0, y1, y2, y0, 1, 2, 0, i);
                            }
                        }
                    }
                    // v2 < v0
                    else
                    {
                        if (y1 > 0 && y0 < 0 && y2 < 0)
                        {
                            // 1 0 2
                            TwoUnderWater(P1, P0, P2, y1, y0, y2, 1, 0, 2, i);
                        }
                        else if (y1 > 0 && y0 > 0 && y2 < 0)
                        {
                            // 1 0 2
                            OneUnderWater(P1, P0, P2, y1, y0, y2, 1, 0, 2, i);
                        }
                    }
                }
            }
        }

        private Vector3 CalculateForces(Vector3 p0, Vector3 p1, Vector3 p2, float dist0, float dist1, float dist2, int index, 
            out Vector3 center, out float area)
        {
            center = (p0 + p1 + p2) / 3f;
            Vector3 u = p1 - p0;
            Vector3 v = p2 - p0;
            Vector3 crossUV = Vector3.Cross(u, v);
            float crossMagnitude = Mathf.Sqrt(crossUV.x * crossUV.x + crossUV.y * crossUV.y + crossUV.z * crossUV.z);
            Vector3 normal = crossMagnitude < 0.00001f ? Vector3.zero : crossUV / crossMagnitude;
            Normals[index] = normal;

            Vector3 p = center - RigidbodyCOMs[_vi0]; // TODO - This should be triangle-base, not vert-base.
            Vector3 velocity = Vector3.Cross(RigidbodyAngularVels[_vi0], p) + RigidbodyLinearVels[_vi0];
            Vector3 waterNormalVector = GlobalUpVector;

            area = crossMagnitude * 0.5f;
            float distanceToSurface;
            if (area > 0.0000001f)
            {
                Vector3 f0 = p0 - center;
                Vector3 f1 = p1 - center;
                Vector3 f2 = p2 - center;
                float doubleArea = area * 2f;
                float w0 = Vector3.Cross(f1, f2).magnitude / doubleArea;
                float w1 = Vector3.Cross(f2, f0).magnitude / doubleArea;
                float w2 = 1f - (w0 + w1);
                Debug.Assert(w0 + w1 + w2 > 0.95f && w0 + w1 + w2 < 1.05f);

                if (SimulateWaterNormals)
                {
                    Vector3 n0 = WaterNormals[_vi0];
                    Vector3 n1 = WaterNormals[_vi1];
                    Vector3 n2 = WaterNormals[_vi2];
                    
                    distanceToSurface =
                        w0 * dist0 * Vector3.Dot(n0, GlobalUpVector) +
                        w1 * dist1 * Vector3.Dot(n1, GlobalUpVector) +
                        w2 * dist2 * Vector3.Dot(n2, GlobalUpVector);

                    waterNormalVector = w0 * n0 + w1 * n1 + w2 * n2;
                }
                else
                {
                    distanceToSurface =
                        w0 * dist0 +
                        w1 * dist1 +
                        w2 * dist2;
                }

                if (SimulateWaterFlow)
                {
                    velocity += w0 * -WaterFlows[_vi0] + 
                                w1 * -WaterFlows[_vi1] +
                                w2 * -WaterFlows[_vi2];
                }
            }
            else
            {
                States[index] = 2;
                return Vector3.zero;
            }
            
            
            float velocityMagnitude = Vector3.Magnitude(velocity);
            Vector3 velocityNormalized = Vector3.Normalize(velocity);
            
            Velocities[index] = velocity;
            Areas[index] = area;
            
            distanceToSurface = distanceToSurface < 0 ? 0 : distanceToSurface;
            Distances[index] = distanceToSurface;

            float densityArea = FluidDensity * area;

            // Buoyant force
            Vector3 buoyantForce = ZeroVector;
            if (CalculateBuoyantForces)
            {
                buoyantForce = distanceToSurface * densityArea * Gravity.y * waterNormalVector * Vector3.Dot(normal, waterNormalVector);
            }

            Vector3 dynamicForce = ZeroVector;
            float dot = Vector3.Dot(normal, velocityNormalized);
            if (CalculateDynamicForces)
            {
                // Dynamic force
                if (dot < -0.0001f || dot > 0.0001f)
                {
                    dot = Mathf.Sign(dot) * Mathf.Pow(Mathf.Abs(dot), VelocityDotPower);
                }
                
                if (DynamicForcePower < 0.9999f || DynamicForcePower > 1.0001f)
                {
                    dynamicForce = -dot * Mathf.Pow(velocityMagnitude, DynamicForcePower) * densityArea * DynamicForceFactor * normal;
                }
                else
                {
                    dynamicForce = -dot * velocityMagnitude * densityArea * DynamicForceFactor * normal;
                }
            }

            if (CalculateSkinDrag)
            {
                float absDot = dot < 0 ? -dot : dot;
                dynamicForce += (1f - absDot) * SkinDrag * densityArea * -velocity;
            }

            return buoyantForce + dynamicForce;
        }
        
        private void ThreeUnderWater(Vector3 p0, Vector3 p1, Vector3 p2, 
                                    float dist0, float dist1, float dist2, 
                                    int i0, int i1, int i2, int index)
        {
            States[index] = 0;

            int i = index * 3;
            P0s[i] = p0;
            P0s[i + 1] = p1;
            P0s[i + 2] = p2;
            
            P1s[i] = Vector3.zero;
            P1s[i + 1] = Vector3.zero;
            P1s[i + 2] = Vector3.zero;

            Vector3 resultForce = CalculateForces(p0, p1, p2, -dist0, -dist1, -dist2, index, out var center, out var area);
            ResultForces[index] = resultForce;
            ResultPoints[index] = center;
        }

        private void TwoUnderWater(Vector3 p0, Vector3 p1, Vector3 p2, 
                                    float dist0, float dist1, float dist2, 
                                    int i0, int i1, int i2, int index)
        {
            States[index] = 1;
            
            Vector3 H, M, L, IM, IL;
            float hH, hM, hL;
            int mIndex;

            // H is always at position 0
            H = p0;

            // Find the index of M
            mIndex = i0 - 1;
            if (mIndex < 0)
            {
                mIndex = 2;
            }

            // Heights to the water
            hH = dist0;

            if (i1 == mIndex)
            {
                M = p1;
                L = p2;

                hM = dist1;
                hL = dist2;
            }
            else
            {
                M = p2;
                L = p1;

                hM = dist2;
                hL = dist1;
            }

            IM = -hM / (hH - hM) * (H - M) + M;
            IL = -hL / (hH - hL) * (H - L) + L;

            int i = index * 3;
            P0s[i] = M;
            P0s[i + 1] = IM;
            P0s[i + 2] = IL; 

            P1s[i] = M;
            P1s[i + 1] = IL;
            P1s[i + 2] = L;

            // Generate tris
            Vector3 force0 = CalculateForces(M, IM, IL, -hM, 0, 0, index, out var center0, out var area0);
            Vector3 force1 = CalculateForces(M, IL, L, -hM, 0, -hL, index, out var center1, out var area1);

            float weight0 = area0 / (area0 + area1);
            ResultForces[index] = force0 + force1;
            ResultPoints[index] = center0 * weight0 + center1 * (1f - weight0);
        }

        private void OneUnderWater(Vector3 p0, Vector3 p1, Vector3 p2, 
            float dist0, float dist1, float dist2, 
            int i0, int i1, int i2, int index)
        {
            States[index] = 1;
            
            Vector3 H, M, L, JM, JH;
            float hH, hM, hL;

            L = p2;

            // Find the index of H
            int hIndex = i2 + 1;
            if (hIndex > 2)
            {
                hIndex = 0;
            }

            // Get heights to water
            hL = dist2;

            if (i1 == hIndex)
            {
                H = p1;
                M = p0;
                
                hH = dist1;
                hM = dist0;
            }
            else
            {
                H = p0;
                M = p1;

                hH = dist0;
                hM = dist1;
            }

            JM = -hL / (hM - hL) * (M - L) + L;
            JH = -hL / (hH - hL) * (H - L) + L;

            int i = index * 3;
            P0s[i] = L;
            P0s[i + 1] = JH;
            P0s[i + 2] = JM;
            
            P1s[i] = Vector3.zero;
            P1s[i + 1] = Vector3.zero;
            P1s[i + 2] = Vector3.zero;
            
            // Generate tris
            Vector3 resultForce = CalculateForces(L, JH, JM, -hL, 0, 0, index, out var center, out var area);
            ResultForces[index] = resultForce;
            ResultPoints[index] = center;
        }
    }
}
