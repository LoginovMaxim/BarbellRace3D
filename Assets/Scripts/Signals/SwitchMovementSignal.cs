using Views;

namespace Signals
{
    public sealed class SwitchMovementSignal
    {
        public MovementType MovementType { get; }

        public SwitchMovementSignal(MovementType movementType)
        {
            MovementType = movementType;
        }
    }
}