using App.Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace App.Services
{
    public sealed class IceMovementSystem : MovementSystem, IIceMovementSystem
    {
        public override MovementType MovementType => MovementType.Ice;

        private Vector3 _movement = Vector3.forward;
        private float _horizontal;
        
        public IceMovementSystem(
            IInputService inputService, 
            IGameConfigProvider gameConfigProvider, 
            IPlayerViewModel playerViewModel, 
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(inputService, gameConfigProvider, playerViewModel, monoUpdater, updateType, isImmediateStart)
        {
        }

        protected override void Update()
        {
            PlayerViewModel.Transform.Translate(_movement * Time.deltaTime);
            PlayerViewModel.IsRun = InputService.IsInputActive;

            if (!InputService.IsInputActive)
            {
                return;
            }

            _horizontal = Mathf.Lerp(_horizontal, InputService.Horizontal, GameConfigProvider.PlayerIceFriction * Time.deltaTime);
            _movement = Vector3.forward * GameConfigProvider.PlayerIceForwardSpeed;
            _movement += Vector3.right * _horizontal * GameConfigProvider.PlayerIceLateralSpeed * GetWeightCoefficient();
        }

        protected override void OnPaused()
        {
        }

        protected override void OnUnPaused()
        {
        }
    }
}