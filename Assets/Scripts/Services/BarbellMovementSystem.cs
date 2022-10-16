using DG.Tweening;
using Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace Services
{
    public sealed class BarbellMovementSystem : UpdatableService, IBarbellMovementSystem
    {
        private readonly IPlayerViewModel _playerViewModel;
        private readonly IBarbellTrackViewModel _barbellTrackViewModel;
        private readonly IGameConfigProvider _gameConfigProvider;
        private readonly BarbellView _barbellView;

        private BarbellPositionType _barbellPositionType;
        private float _force = 100;
        private float _slowMotionScaleTime = 0.2f;
        private float _currentScaleTime = 1f;
        
        public BarbellMovementSystem(
            IPlayerViewModel playerViewModel,
            IBarbellTrackViewModel barbellTrackViewModel,
            IGameConfigProvider gameConfigProvider,
            BarbellView barbellView,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(monoUpdater, updateType, isImmediateStart)
        {
            _playerViewModel = playerViewModel;
            _barbellTrackViewModel = barbellTrackViewModel;
            _gameConfigProvider = gameConfigProvider;
            _barbellView = barbellView;
        }

        protected override void Update()
        {
            switch (_barbellPositionType)
            {
                case BarbellPositionType.Free:
                case BarbellPositionType.Rest:
                {
                    return;
                }
                case BarbellPositionType.Neck:
                {
                    ConstraintToNeck();
                    break;
                }
                case BarbellPositionType.Hand:
                {
                    ConstraintToHands();
                    break;
                }
            }
        }

        protected override void FixedUpdate()
        {
            if (_barbellPositionType != BarbellPositionType.Free)
            {
                return;
            }
            
            MovementOnFinishTrack();
        }

        private void Enable()
        {
            UnPause();

            if (_barbellPositionType != BarbellPositionType.Free)
            {
                return;
            }
            
            _barbellView.Rigidbody.isKinematic = false;
            _barbellView.transform.position = _barbellTrackViewModel.StartPoint.position;
            _barbellView.transform.rotation = _barbellTrackViewModel.StartPoint.rotation;
            _barbellView.transform.DOScale(1.5f * Vector3.one, 2f);
            _barbellView.Rigidbody.constraints = 
                RigidbodyConstraints.FreezePositionX | 
                RigidbodyConstraints.FreezeRotationY | 
                RigidbodyConstraints.FreezeRotationZ;
        }

        private void Disable()
        {
            Pause();
        }

        private void ConstraintToNeck()
        {
            _barbellView.transform.position = _playerViewModel.NeckTransform.position;
            _barbellView.transform.rotation = Quaternion.Lerp(_barbellView.transform.rotation, _playerViewModel.NeckTransform.localRotation, 5*Time.deltaTime);
        }

        private void ConstraintToHands()
        {
            _barbellView.transform.position = _playerViewModel.AverageHandsTransform.position;
            _barbellView.transform.rotation = Quaternion.Lerp(_barbellView.transform.rotation, _playerViewModel.AverageHandsTransform.localRotation, 5*Time.deltaTime);
        }

        private void MovementOnFinishTrack()
        {
            var distance = _barbellTrackViewModel.FinishPoint.position.z - _barbellTrackViewModel.StartPoint.position.z;
            var currentPosition = _barbellView.transform.position.z - _barbellTrackViewModel.StartPoint.position.z;
            var coefficient = 1 - Mathf.Pow(currentPosition / distance, 2);
            
            _barbellView.Rigidbody.velocity = new Vector3(0, _barbellView.Rigidbody.velocity.y, 25 * coefficient);

            var barbellPositionZ = _barbellView.transform.position.z;
            for (var i = 0; i < _barbellTrackViewModel.BarbellTrackDistanceTextViewModels.Count; i++)
            {
                var trackSegmentPositionZ = _barbellTrackViewModel.BarbellTrackDistanceTextViewModels[i].Transform.position.z - 1;

                if (trackSegmentPositionZ > barbellPositionZ)
                {
                    continue;
                }
                
                var distanceToTrack = barbellPositionZ - trackSegmentPositionZ;
                if (distanceToTrack > 4f)
                {
                    _barbellTrackViewModel.BarbellTrackDistanceTextViewModels[i].GroundColor =
                        _gameConfigProvider.FinishGroundColors[2];
                    continue;
                }
                
                if (distanceToTrack > 2f)
                {
                    _barbellTrackViewModel.BarbellTrackDistanceTextViewModels[i].GroundColor =
                        _gameConfigProvider.FinishGroundColors[1];
                    continue;
                }
                
                if (distanceToTrack > 0)
                {
                    _barbellTrackViewModel.BarbellTrackDistanceTextViewModels[i].GroundColor =
                        _gameConfigProvider.FinishGroundColors[0];
                    continue;
                }
            }
        }

        #region IBarbellMovementSystem

        BarbellPositionType IBarbellMovementSystem.BarbellPositionType
        {
            get => _barbellPositionType; 
            set => _barbellPositionType = value;
        }

        void IBarbellMovementSystem.Enable()
        {
            Enable();
        }

        void IBarbellMovementSystem.Disable()
        {
            Disable();
        }

        #endregion
    }
}