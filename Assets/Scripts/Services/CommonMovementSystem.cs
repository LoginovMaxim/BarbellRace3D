using App.Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace App.Services
{
    public sealed class CommonMovementSystem : MovementSystem, ICommonMovementService
    {
        public override MovementType MovementType => MovementType.Common;

        public CommonMovementSystem(
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
            PlayerViewModel.IsRun = InputService.IsInputActive;
            
            var lateralPosition = PlayerViewModel.Transform.position.x;
            if (Mathf.Abs(lateralPosition) > GameConfigProvider.RoadWidth / 2f)
            {
                var offsetDirection = lateralPosition > 0 ? Vector3.left : Vector3.right;
                PlayerViewModel.Transform.Translate(offsetDirection * Time.deltaTime);
                return;
            }

            if (!InputService.IsInputActive)
            {
                return;
            }

            var movement = Vector3.forward * GameConfigProvider.PlayerForwardSpeed;
            
            if (Mathf.Abs(InputService.Horizontal) > GameConfigProvider.PlayerLateralMovementOffset)
            {
                movement += Vector3.right * InputService.Horizontal * GameConfigProvider.PlayerLateralSpeed;
            }

            PlayerViewModel.Transform.Translate(movement * Time.deltaTime);
        }
        
        protected override void OnPaused()
        {
        }

        protected override void OnUnPaused()
        {
            PlayerViewModel.Transform.rotation = Quaternion.identity;
        }
    }
}