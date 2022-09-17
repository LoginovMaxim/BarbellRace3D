using UnityEngine;

namespace Providers
{
    public interface IGameConfigProvider
    {
        float PlayerForwardSpeed { get; }
        float PlayerLateralSpeed { get; }
        float PlayerLateralMovementOffset { get; }
        float PlayerPipeMovementSpeed { get; }
        float PlayerPipeFallSpeed { get; }
        float PlayerIceForwardSpeed { get; }
        float PlayerIceLateralSpeed { get; }
        float PlayerIceFriction { get; }
        float RoadWidth { get; }
        float CameraSmooth { get; }
        Vector3 CameraRunOffset { get; }
    }
}