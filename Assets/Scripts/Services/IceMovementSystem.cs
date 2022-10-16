using Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace Services
{
    public sealed class IceMovementSystem : MovementSystem, IIceMovementSystem
    {
        public override MovementType MovementType => MovementType.Ice;

        private Vector3 _movement = Vector3.forward;
        private float _horizontal;

        private int _inputLag = 4;
        private int _elapsedInputLag = 0;
        
        public IceMovementSystem(
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
            PlayerViewModel.IsRun = InputService.IsInputActive;

            if (!InputService.IsInputActive)
            {
                return;
            }

            if (_elapsedInputLag++ < _inputLag)
            {
                return;
            }
            
            _horizontal = Mathf.Lerp(_horizontal, InputService.Horizontal, GameConfigProvider.PlayerIceFriction * Time.deltaTime);
            _movement = Vector3.forward * GameConfigProvider.PlayerIceForwardSpeed;
            _movement += Vector3.right * _horizontal * GameConfigProvider.PlayerIceLateralSpeed * GetWeightCoefficient();
            _elapsedInputLag = 0;
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