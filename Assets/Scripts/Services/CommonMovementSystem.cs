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
        
        protected override void FixedUpdate()
        {
            PlayerViewModel.IsRun = InputService.IsInputActive;
            
            /*var lateralPosition = PlayerViewModel.Transform.position.x;
            if (Mathf.Abs(lateralPosition) > GameConfigProvider.RoadWidth / 2f)
            {
                var offsetDirection = lateralPosition > 0 ? Vector3.left : Vector3.right;
                PlayerViewModel.Rigidbody.velocity = Vector3.zero;
                PlayerViewModel.Rigidbody.velocity = offsetDirection * (GameConfigProvider.PlayerLateralSpeed / 2) * Time.deltaTime;
                return;
            }*/

            if (!InputService.IsInputActive)
            {
                PlayerViewModel.Rigidbody.velocity = Vector3.zero;
                return;
            }

            var movement = Vector3.forward * GameConfigProvider.PlayerForwardSpeed;
            
            if (Mathf.Abs(InputService.Horizontal) > GameConfigProvider.PlayerLateralMovementOffset)
            {
                movement += Vector3.right * InputService.Horizontal * GameConfigProvider.PlayerLateralSpeed * GetWeightCoefficient();
            }

            PlayerViewModel.Rigidbody.velocity = Vector3.zero;
            //PlayerViewModel.Rigidbody.velocity = movement * Time.deltaTime;
            PlayerViewModel.Rigidbody.AddForce(movement * Time.fixedDeltaTime, ForceMode.VelocityChange);
            Debug.Log($"coefficient {movement}");
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
    }
}