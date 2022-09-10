using App.Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace App.Services
{
    public sealed class CommonMovementService : MovementService, ICommonMovementService
    {
        private readonly IInputService _inputService;
        private readonly IGameConfigProvider _gameConfigProvider;
        private readonly IPlayerViewModel _playerViewModel;
        
        public CommonMovementService(
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
            
            var lateralPosition = _playerViewModel.Transform.position.x;
            if (Mathf.Abs(lateralPosition) > _gameConfigProvider.RoadWidth / 2f)
            {
                var offsetDirection = lateralPosition > 0 ? Vector3.left : Vector3.right;
                _playerViewModel.Transform.Translate(offsetDirection * Time.deltaTime);
                return;
            }

            if (!_inputService.IsInputActive)
            {
                return;
            }

            var movement = Vector3.forward * _gameConfigProvider.PlayerForwardSpeed;
            
            if (Mathf.Abs(_inputService.Horizontal) > _gameConfigProvider.PlayerLateralMovementOffset)
            {
                movement += Vector3.right * _inputService.Horizontal * _gameConfigProvider.PlayerLateralSpeed;
            }

            _playerViewModel.Transform.Translate(movement * Time.deltaTime);
        }

        #region IMovement

        protected override MovementType MovementType => MovementType.Common;

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