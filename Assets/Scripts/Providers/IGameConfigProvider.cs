using System;
using System.Collections.Generic;
using Monos;
using UnityEngine;

namespace Providers
{
    public interface IGameConfigProvider
    {
        float PlayerForwardSpeed { get; }
        float PlayerLateralSpeed { get; }
        float PlayerLimitSpeed { get; }
        float PlayerGravitationForce { get; }
        float PlayerLateralMovementOffset { get; }
        float PlayerPipeMovementSpeed { get; }
        float PlayerPipeFallSpeed { get; }
        float PlayerIceForwardSpeed { get; }
        float PlayerIceLateralSpeed { get; }
        float PlayerIceFriction { get; }
        Vector3 PlayerGuidesStartPosition { get; }
        float PlayerGuidesMovementSpeed { get; }
        float RoadWidth { get; }
        float CameraSmooth { get; }
        List<CameraData> CameraData { get; }
        List<Color> FinishGroundColors { get; }
    }

    [Serializable] public struct CameraData
    {
        public CameraMode CameraMode;
        public Vector3 Offset;
        public Vector3 Rotation;
    }
}