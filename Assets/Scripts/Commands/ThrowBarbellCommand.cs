using System;
using Monos;
using Services;
using Signals;
using Views;
using Zenject;

namespace Commands
{
    public sealed class ThrowBarbellCommand : IDisposable
    {
        private readonly IBarbellMovementSystem _barbellMovementSystem;
        private readonly ICameraFollow _cameraFollow;
        private readonly BarbellView _barbellView;
        private readonly SignalBus _signalBus;

        public ThrowBarbellCommand(
            IBarbellMovementSystem barbellMovementSystem, 
            ICameraFollow cameraFollow,
            BarbellView barbellView,
            SignalBus signalBus)
        {
            _barbellMovementSystem = barbellMovementSystem;
            _cameraFollow = cameraFollow;
            _barbellView = barbellView;
            
            _signalBus = signalBus;
            _signalBus.Subscribe<ThrowBarbellSignal>(OnThrowBarbell);
        }

        private void OnThrowBarbell()
        {
            _barbellMovementSystem.BarbellPositionType = BarbellPositionType.Free;
            _barbellMovementSystem.Enable();
        }

        private void Dispose()
        {
            _signalBus.Unsubscribe<ThrowBarbellSignal>(OnThrowBarbell);
        }

        #region IDisposable
        
        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}