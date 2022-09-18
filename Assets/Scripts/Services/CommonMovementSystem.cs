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

        private Vector3 _movement;
        
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
        
        protected override void FixedUpdate()
        {
            PlayerViewModel.IsRun = InputService.IsInputActive;
            
            if (!InputService.IsInputActive)
            {
                return;
            }

            _movement = PlayerViewModel.Transform.forward;
            
            if (Mathf.Abs(InputService.Horizontal) > GameConfigProvider.PlayerLateralMovementOffset)
            {
                _movement += PlayerViewModel.Transform.right * InputService.Horizontal;
            }

            _movement.Normalize();
            
            var forwardDirection = Vector3.Project(_movement, Vector3.forward);
            var lateralDirection = Vector3.Project(_movement, Vector3.right);

            var forwardVelocity = forwardDirection * GameConfigProvider.PlayerForwardSpeed * Time.fixedDeltaTime;
            var lateralVelocity = lateralDirection * GameConfigProvider.PlayerLateralSpeed * Time.fixedDeltaTime;

            PlayerViewModel.Rigidbody.MovePosition(PlayerViewModel.Transform.position + forwardVelocity + lateralVelocity);
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
        
        protected override void OnPaused()
        {
            PlayerViewModel.Rigidbody.isKinematic = true;
        }

        protected override void OnUnPaused()
        {
            PlayerViewModel.Rigidbody.isKinematic = false;
            PlayerViewModel.Transform.rotation = Quaternion.identity;
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