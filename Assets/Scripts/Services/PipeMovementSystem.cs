using App.Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace App.Services
{
    public sealed class PipeMovementSystem : MovementSystem, IPipeMovementSystem
    {
        public override MovementType MovementType => MovementType.Pipe;

        private float _angle;
        
        public PipeMovementSystem(
            IInputService inputService,
            IGameConfigProvider gameConfigProvider,
            IPlayerViewModel playerViewModel,
            ICameraFollow cameraFollow,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(inputService, gameConfigProvider, playerViewModel, cameraFollow, monoUpdater, updateType, isImmediateStart)
        {
        }

        protected override void Update()
        {
            PlayerViewModel.IsRun = InputService.IsInputActive;

            var playerRotationZ = PlayerViewModel.Transform.localRotation.eulerAngles.z;

            if (playerRotationZ > 180)
            {
                playerRotationZ -= 360;
            }
            
            var angle = _angle + playerRotationZ * GameConfigProvider.PlayerPipeMovementSpeed * Time.deltaTime;
            PlayerViewModel.Transform.RotateAround(PlayerViewModel.TubeRoundParent.position, Vector3.forward, angle);

            _angle = 0;
            if (!InputService.IsInputActive)
            {
                return;
            }

            var centerPosition = PlayerViewModel.Transform.position;
            centerPosition.x = 0;
            
            var movement = Vector3.forward * GameConfigProvider.PlayerPipeMovementSpeed;
            movement += (centerPosition - PlayerViewModel.Transform.position) * GameConfigProvider.PlayerLateralSpeed * GetWeightCoefficient();

            PlayerViewModel.Transform.Translate(movement * Time.deltaTime);
            _angle = -InputService.Horizontal * GameConfigProvider.PlayerPipeFallSpeed * Time.deltaTime;
        }

        protected override void OnEnabled()
        {
            // nothing
        }
        
        protected override void OnDisabled()
        {
            // nothing
        }

        protected override void OnInputStarted()
        {
            // nothing
        }

        protected override void OnInputEnded()
        {
            // nothing
        }
    }
}