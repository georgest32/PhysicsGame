using NWH.NUI;
using UnityEditor;

namespace DWP2.ShipController
{
    [CustomEditor(typeof(Submarine))]
    [CanEditMultipleObjects]
    public class SubmarineEditor : DWP_NUIEditor
    {
        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI())
            {
                return false;
            }

            drawer.Field("requestedDepth");
            drawer.Field("inputDepthChangeSpeed");
            drawer.Field("depthSensitivity");
            drawer.Field("maxMassFactor");
            drawer.Field("keepHorizontal");
            drawer.Field("keepHorizontalSensitivity");
            drawer.Field("maxMassOffset");

            drawer.EndEditor(this);
            return true;
        }
    }
}