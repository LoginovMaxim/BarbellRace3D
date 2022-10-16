using Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace Services
{
    public sealed class RailMovementSystem : MovementSystem, IRailMovementSystem
    {
        public override MovementType MovementType => MovementType.Rails;

        private readonly GuidesView _guidesView;

        private float _speed;
        private float _progress;
        private bool _isJumped;
        
        public RailMovementSystem(
            GuidesView guidesView,
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

            if (_isJumped)
            {
                return;
            }

            if (InputService.Horizontal == 0)
            {
                return;
            }

            if (InputService.Horizontal < 0)
            {
                if (_guidesView.Handle.transform.localPosition.x < -1)
                {
                    return;
                }
                _guidesView.HandleOffset += 1.5f * Vector3.left;
            }
            else if (InputService.Horizontal > 0)
            {
                if (_guidesView.Handle.transform.localPosition.x > 1)
                {
                    return;
                }
                _guidesView.HandleOffset += 1.5f * Vector3.right;
            }

            _isJumped = true;
        }

        protected override void OnEnabled()
        {
            PlayerViewModel.Transform.parent = _guidesView.Handle.transform;
            BarbellView.BarbellParent.localPosition = new Vector3(0f, 0.4f, 0f);
            
            CameraFollow.SetCameraMode(CameraMode.Guides);
        }

        protected override void OnDisabled()
        {
            PlayerViewModel.Transform.parent = null;
            BarbellView.BarbellParent.localPosition = Vector3.zero;
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