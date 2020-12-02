using NWH.NUI;
using UnityEditor;

namespace DWP2.ShipController
{
    [CustomEditor(typeof(Anchor))]
    [CanEditMultipleObjects]
    public class AnchorEditor : DWP_NUIEditor
    {
        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI())
            {
                return false;
            }

            drawer.Field("dropOnStart");
            drawer.Field("forceCoefficient");
            drawer.Field("zeroForceRadius");
            drawer.Field("dragForce");
            drawer.Field("localAnchorPoint");

            drawer.EndEditor(this);
            return true;
        }
    }
}