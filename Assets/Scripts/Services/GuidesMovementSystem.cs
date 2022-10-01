using Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace Services
{
    public sealed class GuidesMovementSystem : MovementSystem, IGuidesMovementSystem
    {
        public override MovementType MovementType => MovementType.Guides;

        private readonly GuidesView _guidesView;

        private float _speed;
        private float _progress;
        private bool _isJumped;
        
        public GuidesMovementSystem(
            GuidesView guidesView,
            IInputService inputService, 
            IGameConfigProvider gameConfigProvider, 
            IPlayerViewModel playerViewModel, 
            ICameraFollow cameraFollow,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(inputService, gameConfigProvider, playerViewModel, cameraFollow, monoUpdater, updateType, isImmediateStart)
        {
            _guidesView = guidesView;
        }

        protected override void Update()
        {
            PlayerViewModel.IsRun = false;

            _speed = Mathf.Lerp(_speed, GameConfigProvider.PlayerGuidesMovementSpeed, Time.deltaTime);
            _progress += _speed * Time.deltaTime;
            _guidesView.Progress = _progress;
            
            PlayerViewModel.Transform.localPosition = GameConfigProvider.PlayerGuidesStartPosition;
            
            if (!InputService.IsInputActive)
            {
                return;
            }

            if (Mathf.Abs(InputService.Horizontal) < GameConfigProvider.PlayerLateralMovementOffset)
            {
                return;
            }

            if (_isJumped)
            {
                return;
            }

            if (InputService.Horizontal < 0)
            {
                if (_guidesView.Handle.transform.localPosition.x < -1)
                {
                    return;
                }
                _guidesView.HandleOffset += 2 * Vector3.left;
            }
            else if (InputService.Horizontal > 0)
            {
                if (_guidesView.Handle.transform.localPosition.x > 1)
                {
                    return;
                }
                _guidesView.HandleOffset += 2 * Vector3.right;
            }

            _isJumped = true;
        }

        protected override void OnEnabled()
        {
            PlayerViewModel.Transform.parent = _guidesView.Handle.transform;
            PlayerViewModel.BarbellParent.localPosition = new Vector3(0.4f, 0f, 0f);
            
            CameraFollow.SetCameraMode(CameraMode.Guides);
        }

        protected override void OnDisabled()
        {
            PlayerViewModel.Transform.parent = null;
            PlayerViewModel.BarbellParent.localPosition = Vector3.zero;

            /*var playerPosition = PlayerViewModel.Transform.position;
            if (PlayerViewModel.Transform.position.x < -1)
            {
                playerPosition.x = -2;
                playerPosition.y = GameConfigProvider.PlayerGuidesStartPosition.y;
            }
            else if (PlayerViewModel.Transform.position.x > 1)
            {
                playerPosition.x = 2;
                playerPosition.y = GameConfigProvider.PlayerGuidesStartPosition.y;
            }
            else
            {
                playerPosition.x = 0;
                playerPosition.y = GameConfigProvider.PlayerGuidesStartPosition.y;
            }

            PlayerViewModel.Transform.position = playerPosition;*/
        }

        protected override void OnInputStarted()
        {
        }

        protected override void OnInputEnded()
        {
            _isJumped = false;
        }
    }
}