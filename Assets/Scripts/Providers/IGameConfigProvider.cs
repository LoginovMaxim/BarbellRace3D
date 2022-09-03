using UnityEngine;

namespace Providers
{
    public interface IGameConfigProvider
    {
        float PlayerLateralSpeed { get; }
        float PlayerForwardSpeed { get; }
        float RoadWidth { get; }
        float CameraSmooth { get; }
        Vector3 CameraRunOffset { get; }
    }
}