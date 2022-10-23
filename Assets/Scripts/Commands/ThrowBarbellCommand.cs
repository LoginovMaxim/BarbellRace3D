using Services;
using Signals;
using UnityEngine;
using Utils;
using ViewModels;
using Zenject;

namespace Commands
{
    public sealed class ThrowBarbellCommand : Command
    {
        private readonly IPlayerViewModel _playerViewModel;
        private readonly IBarbellMovementSystem _barbellMovementSystem;
        private readonly IBarbellTrackViewModel _barbellTrackViewModel;

        public ThrowBarbellCommand(
            IPlayerViewModel playerViewModel,
            IBarbellMovementSystem barbellMovementSystem,
            IBarbellTrackViewModel barbellTrackViewModel,
            SignalBus signalBus) : 
            base(signalBus)
        {
            _playerViewModel = playerViewModel;
            _barbellMovementSystem = barbellMovementSystem;
            _barbellTrackViewModel = barbellTrackViewModel;
        }

        protected override void Subscribe()
        {
            SignalBus.Subscribe<ThrowBarbellSignal>(OnThrowBarbell);
        }

        protected override void Unsubscribe()
        {
            SignalBus.Unsubscribe<ThrowBarbellSignal>(OnThrowBarbell);
        }

        private void OnThrowBarbell()
        {
            var trackCoefficient = ProjectConstants.Gameplay.DefaultFinishDistance / ProjectConstants.Gameplay.MaxDiskCount;
            var finishDistance = _playerViewModel.DiskCount * trackCoefficient;
            var finishPosition = new Vector3(0, 1, finishDistance);
            
            _barbellTrackViewModel.SetFinishPosition(finishPosition);
            _barbellMovementSystem.BarbellPositionType = BarbellPositionType.Free;
            _barbellMovementSystem.Enable();
        }
    }
}