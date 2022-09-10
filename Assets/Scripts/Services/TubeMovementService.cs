using App.Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace App.Services
{
    public sealed class TubeMovementService : MovementService, ITubeMovementService
    {
        private readonly IInputService _inputService;
        private readonly IGameConfigProvider _gameConfigProvider;
        private readonly IPlayerViewModel _playerViewModel;
        
        public TubeMovementService(
            IInputService inputService,
            IGameConfigProvider gameConfigProvider,
            IPlayerViewModel playerViewModel,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(monoUpdater, updateType, isImmediateStart)
        {
            _inputService = inputService;
            _gameConfigProvider = gameConfigProvider;
            _playerViewModel = playerViewModel;
        }

        protected override void Update()
        {
            _playerViewModel.IsRun = _inputService.IsInputActive;

            if (!_inputService.IsInputActive)
            {
                return;
            }

            var centerPosition = _playerViewModel.Transform.position;
            centerPosition.x = 0;
            
            var movement = Vector3.forward * _gameConfigProvider.PlayerForwardSpeed * 0.25f;
            movement += (centerPosition - _playerViewModel.Transform.position) * _gameConfigProvider.PlayerLateralSpeed;

            _playerViewModel.Transform.Translate(movement * Time.deltaTime);
        }

        #region IMovement

        protected override MovementType MovementType => MovementType.Tube;

        void IMovement.Pause()
        {
            Pause();
        }

        void IMovement.UnPause()
        {
            UnPause();
        }

        #endregion
    }
}