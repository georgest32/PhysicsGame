using NWH.NUI;
using UnityEditor;
using UnityEngine;

namespace DWP2.ShipController
{
    [CustomPropertyDrawer(typeof(Rudder))]
    public class RudderDrawer : DWP_NUIPropertyDrawer
    {
        public override bool OnNUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!base.OnNUI(position, property, label))
            {
                return false;
            }

            drawer.Field("name");
            drawer.Field("rudderTransform");
            drawer.Field("maxAngle");
            drawer.Field("rotationSpeed");
            drawer.Field("localRotationAxis");
            
            drawer.EndSubsection();

            drawer.EndProperty();
            return true;
        }
    }
}