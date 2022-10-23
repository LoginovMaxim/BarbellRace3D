using System;
using Monos;
using Services;
using Signals;
using ViewModels;
using Views;
using Zenject;

namespace Commands
{
    public sealed class FinishBarbellTrackCommand : Command
    {
        private readonly IPlayerViewModel _playerViewModel;
        private readonly IBarbellMovementSystem _barbellMovementSystem;
        private readonly ICameraFollow _cameraFollow;
        private readonly BarbellView _barbellView;

        public FinishBarbellTrackCommand(
            IPlayerViewModel playerViewModel, 
            IBarbellMovementSystem barbellMovementSystem,
            ICameraFollow cameraFollow,
            BarbellView barbellView,
            SignalBus signalBus) : 
            base(signalBus)
        {
            _playerViewModel = playerViewModel;
            _barbellMovementSystem = barbellMovementSystem;
            _cameraFollow = cameraFollow;
            _barbellView = barbellView;
        }

        protected override void Subscribe()
        {
            SignalBus.Subscribe<FinishBarbellTrackSignal>(OnFinishProcess);
        }

        protected override void Unsubscribe()
        {
            SignalBus.Unsubscribe<FinishBarbellTrackSignal>(OnFinishProcess);
        }

        private void OnFinishProcess()
        {
            _playerViewModel.IsRun = false;
            _playerViewModel.IsThrow = true;
            _barbellMovementSystem.BarbellPositionType = BarbellPositionType.Hand;
            
            _cameraFollow.SetTarget(_barbellView.transform);
            _cameraFollow.SetCameraMode(CameraMode.BarbellTrack);
            
            _barbellMovementSystem.Enable();
        }
    }
}