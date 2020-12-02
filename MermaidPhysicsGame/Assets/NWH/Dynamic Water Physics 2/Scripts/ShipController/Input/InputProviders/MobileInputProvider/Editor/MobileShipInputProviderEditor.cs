using NWH.NUI;
using UnityEditor;

namespace DWP2.Input
{
    /// <summary>
    ///     Editor for MobileInputProvider.
    /// </summary>
    [CustomEditor(typeof(MobileShipInputProvider))]
    public class MobileShipInputProviderEditor : DWP_NUIEditor
    {
        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI())
            {
                return false;
            }

            drawer.Info("None of the buttons are mandatory. If you do not wish to use an input leave the field empty.");

            MobileShipInputProvider mip = target as MobileShipInputProvider;
            if (mip == null)
            {
                drawer.EndEditor(this);
                return false;
            }

            drawer.BeginSubsection("Vehicle");
            drawer.Field("steeringSlider");
            drawer.Field("throttleSlider");
            drawer.Field("bowThrusterSlider");
            drawer.Field("sternThrusterSlider");
            drawer.Field("submarineDepthSlider");
            drawer.Field("engineStartStopButton");
            drawer.Field("anchorButton");
            drawer.EndSubsection();

            drawer.EndEditor(this);
            return true;
        }

        public override bool UseDefaultMargins()
        {
            return false;
        }
    }
}