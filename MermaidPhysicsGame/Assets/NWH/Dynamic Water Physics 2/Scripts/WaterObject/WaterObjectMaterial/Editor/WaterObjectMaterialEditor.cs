using NWH.NUI;
using UnityEditor;

namespace DWP2
{
    [CustomEditor(typeof(WaterObjectMaterial))]
    [CanEditMultipleObjects]
    public class WaterObjectMaterialEditor : DWP_NUIEditor
    {
        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI())
            {
                return false;
            }

            drawer.Field("density");
            
            drawer.EndEditor(this);
            return true;
        }
    }
}