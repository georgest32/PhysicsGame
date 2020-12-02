using UnityEngine;

namespace NWH.Common.Cameras
{
    /// <summary>
    ///     Empty component that should be attached to the cameras that are inside the vehicle if interior sound change is to
    ///     be used.
    /// </summary>
    public class CameraInsideVehicle : MonoBehaviour
    {
        /// <summary>
        ///     Is the camera inside vehicle?
        /// </summary>
        [Tooltip("    Is the camera inside vehicle?")]
        public bool isInsideVehicle = true;
    }
}