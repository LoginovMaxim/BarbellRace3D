using System;
using Services;
using Signals;
using Views;
using Zenject;

namespace Commands
{
    public sealed class SetRailViewCommand : Command
    {
        private readonly IRailMovementSystem _railMovementSystem;

        public SetRailViewCommand(IRailMovementSystem railMovementSystem, SignalBus signalBus) : base(signalBus)
        {
            _railMovementSystem = railMovementSystem;
        }

        protected override void Subscribe()
        {
            SignalBus.Subscribe<SetRailViewSignal>(s => OnSetRailView(s.RailView));
        }

        protected override void Unsubscribe()
        {
            SignalBus.Unsubscribe<SetRailViewSignal>(s => OnSetRailView(s.RailView));
        }

        private void OnSetRailView(RailView railView)
        {
            _railMovementSystem.SetRailView(railView);
        }
    }
}