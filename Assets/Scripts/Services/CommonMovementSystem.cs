using Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace Services
{
    public sealed class CommonMovementSystem : MovementSystem, ICommonMovementSystem
    {
        public override MovementType MovementType => MovementType.Common;

        private Vector3 _movement;
        
        public CommonMovementSystem(
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
        
        protected override void FixedUpdate()
        {
            PlayerViewModel.IsRun = InputService.IsInputActive;
            
            if (!InputService.IsInputActive)
            {
                return;
            }

            _movement = PlayerViewModel.Transform.forward * GameConfigProvider.PlayerForwardSpeed;
            
            if (Mathf.Abs(InputService.Horizontal) > GameConfigProvider.PlayerLateralMovementOffset)
            {
                _movement += PlayerViewModel.Transform.right * GameConfigProvider.PlayerLateralSpeed * InputService.Horizontal;
            }

            PlayerViewModel.Rigidbody.velocity = new Vector3(_movement.x, PlayerViewModel.Rigidbody.velocity.y, _movement.z);
        }

        private void ControlSpeed()
        {
            var planeVelocity = new Vector3(PlayerViewModel.Rigidbody.velocity.x, 0f, PlayerViewModel.Rigidbody.velocity.z);

            if (planeVelocity.magnitude < GameConfigProvider.PlayerLateralSpeed)
            {
                return;
            }

            planeVelocity = planeVelocity.normalized * GameConfigProvider.PlayerLateralSpeed;
            PlayerViewModel.Rigidbody.velocity = new Vector3(planeVelocity.x, PlayerViewModel.Rigidbody.velocity.y, planeVelocity.z);
        }

        protected override void OnEnabled()
        {
            PlayerViewModel.Rigidbody.isKinematic = false;
            PlayerViewModel.Transform.rotation = Quaternion.identity;
            
            CameraFollow.SetCameraMode(CameraMode.Run);
        }
        
        protected override void OnDisabled()
        {
            PlayerViewModel.Rigidbody.isKinematic = true;
        }

        protected override void OnInputStarted()
        {
            // nothing
        }

        protected override void OnInputEnded()
        {
            PlayerViewModel.Rigidbody.velocity = Vector3.zero;
        }
    }
}