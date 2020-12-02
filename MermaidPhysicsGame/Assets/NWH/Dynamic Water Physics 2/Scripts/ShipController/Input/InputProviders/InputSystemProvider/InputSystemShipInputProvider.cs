using UnityEngine;

namespace DWP2.Input
{
    /// <summary>
    /// Class for handling input through new InputSystem
    /// </summary>
    public class InputSystemShipInputProvider : ShipInputProvider
    {
        public ShipInputActions shipInputActions;

        private float _steering;
        private float _throttle;
        private float _sternThruster;
        private float _bowThruster;
        private float _submarineDepth;

        public new void Awake()
        {
            base.Awake();
            shipInputActions = new ShipInputActions();
            shipInputActions.Enable();
            
            //TODO
            //shipInputActions.SceneControls.DragObjectModifier.started += ctx => _dragObjectModifier = true;
            //shipInputActions.SceneControls.DragObjectModifier.canceled += ctx => _dragObjectModifier = false;
            
        }

        public void Update()
        {
            _steering = shipInputActions.ShipControls.Steering.ReadValue<float>();
            _throttle = shipInputActions.ShipControls.Throttle.ReadValue<float>();
            _bowThruster = shipInputActions.ShipControls.BowThruster.ReadValue<float>();
            _sternThruster = shipInputActions.ShipControls.SternThruster.ReadValue<float>();
            _submarineDepth = shipInputActions.ShipControls.SubmarineDepth.ReadValue<float>();
        }
        
        // Ship bindings
        public override float Throttle() => _throttle;                      
        public override float Steering() => _steering;
        public override float BowThruster() => _bowThruster;
        public override float SternThruster() => _sternThruster;
        public override float SubmarineDepth() => _submarineDepth;
        public override bool EngineStartStop() => shipInputActions.ShipControls.EngineStartStop.triggered;
        public override bool Anchor() => shipInputActions.ShipControls.Anchor.triggered;

        public override Vector2 DragObjectPosition() => Vector2.zero;
        public override bool DragObjectModifier() => false;
    }
}