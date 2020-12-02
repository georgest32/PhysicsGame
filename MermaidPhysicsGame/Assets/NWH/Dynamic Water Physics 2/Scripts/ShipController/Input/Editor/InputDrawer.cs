using UnityEditor;
using UnityEngine;
using NWH.NUI;

namespace DWP2
{
    /// <summary>
    ///     Property drawer for Input.
    /// </summary>
    [CustomPropertyDrawer(typeof(DWP2.Input.ShipInputHandler))]
    public class InputDrawer : DWP_NUIPropertyDrawer
    {
        private float infoHeight;

        public override bool OnNUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!base.OnNUI(position, property, label))
            {
                return false;
            }

            drawer.Field("autoSetInput");
            drawer.Field("states");

            drawer.EndProperty();
            return true;
        }
    }
}