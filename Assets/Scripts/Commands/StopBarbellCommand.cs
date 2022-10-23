using Services;
using Signals;
using Views;
using Zenject;

namespace Commands
{
    public sealed class StopBarbellCommand : Command
    {
        private readonly IBarbellMovementSystem _barbellMovementSystem;
        private readonly BarbellView _barbellView;

        public StopBarbellCommand(
            IBarbellMovementSystem barbellMovementSystem, 
            BarbellView barbellView, 
            SignalBus signalBus) : 
            base(signalBus)
        {
            _barbellMovementSystem = barbellMovementSystem;
            _barbellView = barbellView;
        }

        private void OnStopBarbellMovement()
        {
            _barbellMovementSystem.BarbellPositionType = BarbellPositionType.Rest;
        }

        protected override void Subscribe()
        {
            SignalBus.Subscribe<StopBarbellSignal>(OnStopBarbellMovement);
        }

        protected override void Unsubscribe()
        {
            SignalBus.Unsubscribe<StopBarbellSignal>(OnStopBarbellMovement);
        }
    }
}