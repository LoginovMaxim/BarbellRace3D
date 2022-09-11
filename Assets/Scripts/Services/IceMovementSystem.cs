using System.Collections.Generic;
using App.Monos;
using ModestTree;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace App.Services
{
    public sealed class IceMovementSystem : MovementSystem, IIceMovementSystem
    {
        public override MovementType MovementType => MovementType.Ice;

        private List<Vector3> _movementBuffer = new List<Vector3>();
        private int _movementBufferSize = 10;
        private float _freezeInputDuration = 0.1f;
        private float _elapsedFreezeTime = 1f;

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
            PlayerViewModel.Transform.Translate(GetAverageMovement() * Time.deltaTime);
            
            PlayerViewModel.IsRun = InputService.IsInputActive;
            
            var lateralPosition = PlayerViewModel.Transform.position.x;
            if (Mathf.Abs(lateralPosition) > GameConfigProvider.RoadWidth)
            {
                var offsetDirection = lateralPosition > 0 ? Vector3.left : Vector3.right;
                AddToMovementBuffer(offsetDirection * Time.deltaTime);
                return;
            }

            if (!InputService.IsInputActive)
            {
                AddToMovementBuffer(Vector3.forward * GameConfigProvider.PlayerForwardSpeed * 0.1f);
                return;
            }

            if (IsFreezeInput())
            {
                return;
            }

            var movement = Vector3.forward * GameConfigProvider.PlayerForwardSpeed * 0.33f;
            
            if (Mathf.Abs(InputService.Horizontal) > GameConfigProvider.PlayerLateralMovementOffset)
            {
                movement += Vector3.right * InputService.Horizontal * GameConfigProvider.PlayerLateralSpeed * 0.5f;
            }
            
            AddToMovementBuffer(movement);
            PlayerViewModel.MovementBuffer = _movementBuffer;
        }

        private void AddToMovementBuffer(Vector3 movement)
        {
            if (_movementBuffer.Count < _movementBufferSize)
            {
                _movementBuffer.Add(movement);
                return;
            }
            
            _movementBuffer.RemoveAt(0);
            _movementBuffer.Add(movement);
        }

        private Vector3 GetAverageMovement()
        {
            if (_movementBuffer.IsEmpty())
            {
                return Vector3.zero;
            }
            
            var averageMovement = Vector3.zero;
            foreach (var movement in _movementBuffer)
            {
                averageMovement += movement;
            }

            return averageMovement / _movementBuffer.Count;
        }

        private bool IsFreezeInput()
        {
            _elapsedFreezeTime += Time.deltaTime;
            if (_elapsedFreezeTime < _freezeInputDuration)
            {
                return true;
            }

            _elapsedFreezeTime = 0;
            return false;
        }

        protected override void OnPaused()
        {
            _movementBuffer.Clear();
        }

        protected override void OnUnPaused()
        {
            _movementBuffer.Clear();
        }
    }
}