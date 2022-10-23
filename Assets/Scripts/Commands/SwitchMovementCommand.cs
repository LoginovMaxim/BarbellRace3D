using System;
using System.Collections.Generic;
using Services;
using Signals;
using Zenject;

namespace Commands
{
    public sealed class SwitchMovementCommand : Command
    {
        private readonly List<IMovement> _movements;

        public SwitchMovementCommand(List<IMovement> movements, SignalBus signalBus) : base(signalBus)
        {
            _movements = movements;
        }

        protected override void Subscribe()
        {
            SignalBus.Subscribe<SwitchMovementSignal>(s => OnSwitchMovement(s.MovementType));
        }

        protected override void Unsubscribe()
        {
            SignalBus.Unsubscribe<SwitchMovementSignal>(s => OnSwitchMovement(s.MovementType));
        }

        private void OnSwitchMovement(MovementType movementType)
        {
            foreach (var movement in _movements)
            {
                if (movement.MovementType == movementType)
                {
                    if (movement.IsEnabled)
                    {
                        continue;
                    }
                    
                    movement.Enable();
                }
                else
                {
                    if (!movement.IsEnabled)
                    {
                        continue;
                    }
                    
                    movement.Disable();
                }
            }
        }
    }
}