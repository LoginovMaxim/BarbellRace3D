using App.Monos;
using Providers;
using UnityEngine;
using ViewModels;

namespace App.Services
{
    public sealed class MovementService : UpdatableService, IMovementService
    {
        private readonly IInputService _inputService;
        private readonly IGameConfigProvider _gameConfigProvider;
        private readonly Transform _target;

        public MovementService(
            IInputService inputService,
            IGameConfigProvider gameConfigProvider,
            PlayerViewModel playerViewModel,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(monoUpdater, updateType, isImmediateStart)
        {
            _inputService = inputService;
            _gameConfigProvider = gameConfigProvider;
            _target = playerViewModel.transform;
        }

        protected override void Update()
        {
            var lateralPosition = _target.transform.position.x;
            if (Mathf.Abs(lateralPosition) > _gameConfigProvider.RoadWidth / 2f)
            {
                var offsetDirection = lateralPosition > 0 ? Vector3.left : Vector3.right;
                _target.transform.Translate(offsetDirection * Time.deltaTime);
                return;
            }

            if (_inputService.Horizontal == 0)
            {
                return;
            }

            var direction = (Vector3.right * _inputService.Horizontal).normalized;
            var totalDirection = direction * _gameConfigProvider.PlayerLateralSpeed +
                                 Vector3.forward * _gameConfigProvider.PlayerForwardSpeed;
            
            _target.transform.Translate(totalDirection * Time.deltaTime);
        }
    }
}