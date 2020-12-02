using NWH.NUI;
using UnityEditor;
using UnityEngine;

namespace DWP2
{
    [CustomEditor(typeof(MassFromVolume))]
    [CanEditMultipleObjects]
    public class MassFromVolumeEditor : DWP_NUIEditor
    {
        MassFromVolume _massFromVolume;

        public void OnEnable()
        {
            _massFromVolume = (MassFromVolume)target;
        }
        
        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI() || _massFromVolume == null)
            {
                return false;
            }

            _massFromVolume = (MassFromVolume) target;

            // Material settings
            drawer.Field("mass", true, "kg");
            drawer.Field("volume", false, "m3");
            drawer.Info("Volume is auto-calculated from the mesh when either Calculate option is used.");

            drawer.BeginSubsection("Density");
            drawer.Field("density", true, "kg/m3");
            if (drawer.Button("Calculate Mass From Density"))
            {
                foreach (MassFromVolume mfm in targets)
                {
                    mfm.CalculateAndApplyFromDensity(mfm.density);
                }
            }
            drawer.EndSubsection();
            
            drawer.BeginSubsection("Material");
            drawer.Field("material");
            if (drawer.Button("Calculate Mass From Material"))
            {
                foreach (MassFromVolume mfm in targets)
                {
                    mfm.CalculateAndApplyFromMaterial();
                }
            }
            drawer.EndSubsection();
            
            drawer.EndEditor(this);
            return true;
        }
    }
}