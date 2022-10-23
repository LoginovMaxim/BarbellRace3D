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

        private RailView _railView;

        private float _speed;
        private float _progress;
        private bool _isJumped;
        
        public RailMovementSystem(
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
            if (_railView == null)
            {
                return;
            }

            _speed = Mathf.Lerp(_speed, GameConfigProvider.PlayerGuidesMovementSpeed, Time.deltaTime);
            _progress += _speed * Time.deltaTime;
            _railView.Progress = _progress;
            
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
                if (_railView.Handle.transform.localPosition.x < -1)
                {
                    return;
                }
                _railView.HandleOffset += 1.5f * Vector3.left;
            }
            else if (InputService.Horizontal > 0)
            {
                if (_railView.Handle.transform.localPosition.x > 1)
                {
                    return;
                }
                _railView.HandleOffset += 1.5f * Vector3.right;
            }

            _isJumped = true;
        }

        protected override void OnEnabled()
        {
            _speed = 0f;
            _progress = 0f;
            _isJumped = false;
            
            PlayerViewModel.IsRun = false;
            PlayerViewModel.Transform.parent = _railView.Handle.transform;
            
            BarbellView.BarbellParent.localPosition = new Vector3(0f, 0.4f, 0f);
            
            CameraFollow.SetCameraMode(CameraMode.Rails);
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
        
        private void SetRailView(RailView railView)
        {
            _railView = railView;
        }

        #region IRailMovementSystem

        void IRailMovementSystem.SetRailView(RailView railView)
        {
            SetRailView(railView);
        }

        #endregion
    }
}