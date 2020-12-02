using NWH.NUI;
using UnityEditor;

namespace DWP2.Input
{
    [CustomEditor(typeof(InputManagerShipInputProvider))]
    public class InputManagerShipInputProviderEditor : DWP_NUIEditor
    {
        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI())
            {
                return false;
            }

            drawer.EndEditor(this);
            return true;
        }

        public override bool UseDefaultMargins()
        {
            return false;
        }
    }
}