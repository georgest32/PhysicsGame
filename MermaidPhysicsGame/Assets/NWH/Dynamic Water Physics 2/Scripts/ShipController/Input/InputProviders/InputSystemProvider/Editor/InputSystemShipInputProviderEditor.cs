using NWH.NUI;
using UnityEditor;

namespace DWP2.Input
{
    [CustomEditor(typeof(InputSystemShipInputProvider))]
    public class InputSystemShipInputProviderEditor : DWP_NUIEditor
    {
        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI())
            {
                return false;
            }
            
            drawer.Info("Input settings for Unity's new input system can be changed by modifying 'ShipInputActions' " +
                        "file (double click on it to open).");

            drawer.EndEditor(this);
            return true;
        }

        public override bool UseDefaultMargins()
        {
            return false;
        }
    }
}