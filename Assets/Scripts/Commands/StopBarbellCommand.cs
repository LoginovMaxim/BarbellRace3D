using System;
using Services;
using Signals;
using Views;
using Zenject;

namespace Commands
{
    public sealed class StopBarbellCommand : IDisposable
    {
        private readonly IBarbellMovementSystem _barbellMovementSystem;
        private readonly BarbellView _barbellView;
        private readonly SignalBus _signalBus;

        public StopBarbellCommand(IBarbellMovementSystem barbellMovementSystem, BarbellView barbellView, SignalBus signalBus)
        {
            _barbellMovementSystem = barbellMovementSystem;
            _barbellView = barbellView;
            
            _signalBus = signalBus;
            _signalBus.Subscribe<StopBarbellSignal>(OnStopBarbellMovement);
        }

        private void OnStopBarbellMovement()
        {
            _barbellMovementSystem.BarbellPositionType = BarbellPositionType.Rest;
        }
        
        private void Dispose()
        {
            _signalBus.Unsubscribe<StopBarbellSignal>(OnStopBarbellMovement);
        }

        #region IDisposable
        
        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}