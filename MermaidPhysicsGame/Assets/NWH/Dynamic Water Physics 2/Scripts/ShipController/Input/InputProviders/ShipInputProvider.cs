using NWH.Common.Input;
 using UnityEngine;

namespace DWP2.Input
{
    /// <summary>
    ///     Base abstract class from which all input providers inherit.
    /// </summary>
    public abstract class ShipInputProvider : InputProvider
    {
        // Ship bindings
        public virtual float Steering() => 0f;
        public virtual float Throttle() => 0f;
        public virtual float SternThruster() => 0f;
        public virtual float BowThruster() => 0f;
        public virtual float SubmarineDepth() => 0f;
        public virtual bool EngineStartStop() => false;
        public virtual bool Anchor() => false;
        
        // Additional scene bindings
        public virtual Vector2 DragObjectPosition() => Vector2.zero;
        public virtual bool DragObjectModifier() => false;
    }
}
