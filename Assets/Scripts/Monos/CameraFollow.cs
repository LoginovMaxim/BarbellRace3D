using Providers;
using UnityEngine;
using ViewModels;
using Views;
using Zenject;

namespace Monos
{
    public sealed class CameraFollow : MonoBehaviour, ICameraFollow
    {
        private Transform _target;
        private IGameConfigProvider _gameConfigProvider;
        private Vector3 _offset;
        private Vector3 _rotation;
        
        [Inject] public void Inject(IPlayerViewModel playerViewModel, IGameConfigProvider gameConfigProvider)
        {
            _target = playerViewModel.Transform;
            _gameConfigProvider = gameConfigProvider;
        }

        private void Start()
        {
            SetCameraMode(CameraMode.Run);
        }

        private void LateUpdate()
        {
            var targetPosition = _target.position + _offset;
            var smooth = _gameConfigProvider.CameraSmooth;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rotation), smooth * Time.deltaTime);
        }

        private void SetTarget(Transform targetTransform)
        {
            _target = targetTransform;
        }

        private void SetCameraMode(CameraMode cameraMode)
        {
            foreach (var cameraData in _gameConfigProvider.CameraData)
            {
                if (cameraData.CameraMode != cameraMode)
                {
                    continue;
                }

                _offset = cameraData.Offset;
                _rotation = cameraData.Rotation;
                return;
            }
        }

        #region ICameraFollow

        void ICameraFollow.SetTarget(Transform targetTransform)
        {
            SetTarget(targetTransform);
        }

        void ICameraFollow.SetCameraMode(CameraMode cameraMode)
        {
            SetCameraMode(cameraMode);
        }

        #endregion
    }
}