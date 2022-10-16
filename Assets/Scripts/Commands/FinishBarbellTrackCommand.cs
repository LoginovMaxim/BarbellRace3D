using System;
using Monos;
using Services;
using Signals;
using ViewModels;
using Views;
using Zenject;

namespace Commands
{
    public sealed class FinishBarbellTrackCommand : IDisposable
    {
        private readonly IPlayerViewModel _playerViewModel;
        private readonly IBarbellMovementSystem _barbellMovementSystem;
        private readonly ICameraFollow _cameraFollow;
        private readonly BarbellView _barbellView;
        private readonly SignalBus _signalBus;

        public FinishBarbellTrackCommand(
            IPlayerViewModel playerViewModel, 
            IBarbellMovementSystem barbellMovementSystem,
            ICameraFollow cameraFollow,
            BarbellView barbellView,
            SignalBus signalBus)
        {
            _playerViewModel = playerViewModel;
            _barbellMovementSystem = barbellMovementSystem;
            _cameraFollow = cameraFollow;
            _barbellView = barbellView;
            
            _signalBus = signalBus;
            _signalBus.Subscribe<FinishBarbellTrackSignal>(OnFinishProcess);
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

        private void Dispose()
        {
            _signalBus.Unsubscribe<FinishBarbellTrackSignal>(OnFinishProcess);
        }

        #region IDisposable
        
        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}