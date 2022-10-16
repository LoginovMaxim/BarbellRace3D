using UnityEngine;

namespace Monos
{
    public interface ICameraFollow
    {
        void SetTarget(Transform targetTransform);
        void SetCameraMode(CameraMode cameraMode);
    }
}