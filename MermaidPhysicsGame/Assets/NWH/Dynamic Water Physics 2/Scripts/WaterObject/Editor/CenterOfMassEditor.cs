using NWH.NUI;
using UnityEditor;

namespace DWP2
{
    [CustomEditor(typeof(CenterOfMass))]
    [CanEditMultipleObjects]
    public class CenterOfMassEditor : DWP_NUIEditor
    {
        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI())
            {
                return false;
            }

            drawer.Field("centerOfMassOffset");
            drawer.Field("showCOM");

            drawer.EndEditor(this);
            return true;
        }
    }
}