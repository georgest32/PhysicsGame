using UnityEditor;
using UnityEngine;
using NWH.NUI;

namespace DWP2.Input
{
    /// <summary>
    ///     Property drawer for InputStates.
    /// </summary>
    [CustomPropertyDrawer(typeof(ShipInputStates))]
    public class InputStatesDrawer : DWP_NUIPropertyDrawer
    {
        public override bool OnNUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!base.OnNUI(position, property, label))
            {
                return false;
            }

            drawer.Field("steering");
            drawer.Field("throttle");
            drawer.Field("bowThruster");
            drawer.Field("sternThruster");
            drawer.Field("submarineDepth");
            drawer.Field("engineStartStop");
            drawer.Field("anchor");
            EditorGUI.EndDisabledGroup();

            drawer.EndProperty();
            return true;
        }
    }
}