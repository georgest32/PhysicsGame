using System;
using NWH.Common.Input;
using UnityEngine;

namespace DWP2.Input
{
    /// <summary>
    ///     Manages vehicle input by retrieving it from the active InputProvider and filling in the InputStates with the
    ///     fetched data.
    /// </summary>
    [Serializable]
    public class ShipInputHandler
    {
        /// <summary>
        /// When enabled input will be auto-retrieved from the InputProviders present in the scene.
        /// Disable to manualy set the input through external scripts, i.e. AI controller.
        /// </summary>
        public bool autoSetInput = true;

        /// <summary>
        /// All the input states of the vehicle. Can be used to set input through scripting or copy the inputs
        /// over from other vehicle, such as truck to trailer.
        /// </summary>
        public ShipInputStates states;

        public float Throttle
        {
            get => states.throttle;
            set => states.throttle = Mathf.Clamp(value, -1f, 1f);
        }

        public float Steering
        {
            get => states.steering;
            set => states.steering = Mathf.Clamp(value, -1f, 1f);
        }

        public float SternThruster
        {
            get => states.sternThruster;
            set => states.sternThruster = Mathf.Clamp(value, -1f, 1f);
        }

        public float BowThruster
        {
            get => states.bowThruster;
            set => states.bowThruster = Mathf.Clamp(value, -1f, 1f);
        }

        public float SubmarineDepth
        {
            get => states.submarineDepth;
            set => states.submarineDepth = Mathf.Clamp01(value);
        }

        public bool Anchor
        {
            get => states.anchor;
            set => states.anchor = value;
        }

        public bool EngineStartStop
        {
            get => states.engineStartStop;
            set => states.engineStartStop = value;
        }

        public void Update()
        {
            if (!autoSetInput)
            {
                return;
            }
            
            Steering = InputProvider.CombinedInput<ShipInputProvider>(i => i.Steering());
            Throttle = InputProvider.CombinedInput<ShipInputProvider>(i => i.Throttle());
            BowThruster = InputProvider.CombinedInput<ShipInputProvider>(i => i.BowThruster());
            SternThruster = InputProvider.CombinedInput<ShipInputProvider>(i => i.SternThruster());
            SubmarineDepth = InputProvider.CombinedInput<ShipInputProvider>(i => i.SubmarineDepth());

            EngineStartStop |= InputProvider.CombinedInput<ShipInputProvider>(i => i.EngineStartStop());
            Anchor |= InputProvider.CombinedInput<ShipInputProvider>(i => i.Anchor());
        }
    }
}