using Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace Services
{
    public sealed class PipeMovementSystem : MovementSystem, IPipeMovementSystem
    {
        public override MovementType MovementType => MovementType.Pipe;

        private Vector3 _movement = Vector3.forward;
        private float _angle;
        
        public PipeMovementSystem(
            IInputService inputService,
            IGameConfigProvider gameConfigProvider,
            IPlayerViewModel playerViewModel,
            ICameraFollow cameraFollow,
            BarbellView barbellView,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(inputService, gameConfigProvider, playerViewModel, cameraFollow, barbellView, monoUpdater, updateType, isImmediateStart)
        {
        }

        protected override void Update()
        {
            PlayerViewModel.Transform.Translate(_movement * Time.deltaTime);
            _movement = Vector3.forward * GameConfigProvider.PlayerPipeMovementSpeed;

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
            
            _movement += (centerPosition - PlayerViewModel.Transform.position) * GameConfigProvider.PlayerLateralSpeed * GetWeightCoefficient();

            _angle = -InputService.Horizontal * GameConfigProvider.PlayerPipeFallSpeed * Time.deltaTime;
        }

        protected override void OnEnabled()
        {
            PlayerViewModel.IsRun = true;
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