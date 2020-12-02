using UnityEngine;

namespace DWP2
{
    /// <summary>
    /// Helper scripts to debug water heights in wavy water assets.
    /// Attach to a transform and press play.
    /// </summary>
    public class WaterDataDebugGrid : MonoBehaviour
    {
        private const int GRID_WIDTH = 10;
        private const int GRID_SIZE = GRID_WIDTH * GRID_WIDTH;

        private Vector3[] _positions;
        private float[] _waterHeights;
        private Vector3[] _waterFlows;
        private Vector3[] _waterNormals;

        void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60; // Cap the frame rate as Crest has an bug with queries and high frame rates.
            
            _positions = new Vector3[GRID_SIZE];
            _waterHeights = new float[GRID_SIZE];
            _waterFlows = new Vector3[GRID_SIZE];
            _waterNormals = new Vector3[GRID_SIZE];
            
            for (int i = 0; i < GRID_WIDTH; i++)
            {
                for (int j = 0; j < GRID_WIDTH; j++)
                {
                    int index = i * GRID_WIDTH + j;
                    _positions[index] = new Vector3(j * 2, 0, i * 2);
                }
            }
        }
        
        void FixedUpdate()
        {
           WaterDataProvider.Instance.GetWaterHeightsFlowsNormals(ref _positions, ref _waterHeights, 
                ref _waterFlows, ref _waterNormals);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            for (int i = 0; i < GRID_SIZE; i++)
            {
                // Draw positions
                Gizmos.color = Color.white;
                Vector3 p1 = _positions[i];
                p1.y = _waterHeights[i];
                Gizmos.DrawSphere(p1, 0.1f);
                
                // Draw flows
                Gizmos.color = Color.red;
                Gizmos.DrawLine(p1, p1 + _waterFlows[i] / 2f);
                
                // Draw normals
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(p1, p1 + _waterNormals[i] * 2f);
            }
        }
    }
}

