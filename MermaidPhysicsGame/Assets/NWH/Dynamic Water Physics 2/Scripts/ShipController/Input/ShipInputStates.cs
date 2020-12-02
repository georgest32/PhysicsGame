using System;
using UnityEngine;

namespace DWP2.Input
{
    /// <summary>
    /// Struct for storing ship input
    /// </summary>
    [Serializable]
    public struct ShipInputStates
    {
        [Range(-1, 1)]
        public float steering;
        
        [Range(-1, 1)]
        public float throttle;

        [Range(-1, 1)]
        public float sternThruster;
        
        [Range(-1, 1)]
        public float bowThruster;
        
        [Range(0, 1)]
        public float submarineDepth;
        
        public bool engineStartStop;
        public bool anchor;

        public bool changeShip;
        public bool changeCamera;
        
        public void Reset()
        {
            throttle = 0;
            sternThruster = 0;
            bowThruster = 0;
            submarineDepth = 0;
            engineStartStop = false;
            anchor = false;
        }
    }
}