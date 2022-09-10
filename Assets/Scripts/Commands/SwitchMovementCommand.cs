using System;
using System.Collections.Generic;
using App.Services;
using Signals;
using Views;
using Zenject;

namespace Commands
{
    public sealed class SwitchMovementCommand : IDisposable
    {
        private readonly List<IMovement> _movements;
        private readonly SignalBus _signalBus;

        public SwitchMovementCommand(List<IMovement> movements, SignalBus signalBus)
        {
            _movements = movements;
            _signalBus = signalBus;
            _signalBus.Subscribe<SwitchMovementSignal>(x => OnSwitchMovement(x.MovementType));
        }

        private void OnSwitchMovement(MovementType movementType)
        {
            foreach (var movement in _movements)
            {
                if (movement.MovementType == movementType)
                {
                    movement.UnPause();
                }
                else
                {
                    movement.Pause();
                }
            }
        }

        private void Dispose()
        {
            _signalBus.Unsubscribe<SwitchMovementSignal>(x => OnSwitchMovement(x.MovementType));
        }

        #region IDisposable

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}