using UnityEngine;

namespace DWP2
{
    public class FlatWaterDataProvider : WaterDataProvider
    {
        public override bool SupportsWaterHeightQueries()
        {
            return false;
        }

        public override bool SupportsWaterNormalQueries()
        {
            return false;
        }

        public override bool SupportsWaterFlowQueries()
        {
            return false;
        }
        
        public override void GetWaterHeights(ref Vector3[] points, ref float[] waterHeights)
        {
            float waterHeight = transform.position.y;
            
            waterHeights.Fill(waterHeight);
        }
    }
}

