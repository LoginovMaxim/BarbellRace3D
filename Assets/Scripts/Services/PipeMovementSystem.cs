using App.Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace App.Services
{
    public sealed class PipeMovementSystem : MovementSystem, IPipeMovementService
    {
        public override MovementType MovementType => MovementType.Tube;
        
        public PipeMovementSystem(
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

            if (!InputService.IsInputActive)
            {
                return;
            }

            var centerPosition = PlayerViewModel.Transform.position;
            centerPosition.x = 0;
            
            var movement = Vector3.forward * GameConfigProvider.PlayerForwardSpeed * 0.25f;
            movement += (centerPosition - PlayerViewModel.Transform.position) * GameConfigProvider.PlayerLateralSpeed;

            PlayerViewModel.Transform.Translate(movement * Time.deltaTime);

            var playerAngle = Mathf.Abs(PlayerViewModel.Transform.rotation.z);
            var angle = -InputService.Horizontal * Time.deltaTime * 10;
            if (playerAngle > 1)
            {
                angle *= playerAngle * 10f;
            }
                
            PlayerViewModel.Transform.RotateAround(PlayerViewModel.TubeRoundParent.position, Vector3.forward, angle);
        }
        
        protected override void OnPaused()
        {
        }

        protected override void OnUnPaused()
        {
        }
    }
}