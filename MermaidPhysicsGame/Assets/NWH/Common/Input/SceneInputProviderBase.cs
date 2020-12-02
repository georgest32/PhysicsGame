using UnityEngine;

namespace NWH.Common.Input
{
    /// <summary>
    /// InputProvider for scene and camera related behavior.
    /// </summary>
    public abstract class SceneInputProviderBase : InputProvider
    {
        // Common camera bindings
        public virtual bool ChangeCamera()
        {
            return false;
        }
        
        public virtual Vector2 CameraRotation()
        {
            return Vector2.zero;
        }
        
        public virtual Vector2 CameraPanning()
        {
            return Vector2.zero;
        }
        
        public virtual bool CameraRotationModifier()
        
        {
            return false;
        }

        public virtual bool CameraPanningModifier()
        
        {
            return false;
        }

        public virtual float CameraZoom()
        {
            return 0;
        }
        
        // Common scene bindings
        public virtual bool ChangeVehicle()
        {
            return false;
        }

        public virtual Vector2 CharacterMovement()
        {
            return Vector2.zero;
        }
        
        public virtual bool ToggleGUI()
        {
            return false;
        }
    }
}