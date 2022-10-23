using System.Collections.Generic;
using Providers;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "BarbellRace3D/GameConfig")]
    public sealed class GameConfig : ScriptableObject, IGameConfigProvider
    {
        [Header("Common movement")]
        [SerializeField] private float _playerForwardSpeed;
        [SerializeField] private float _playerLateralSpeed;
        [SerializeField] private float _playerLimitForce;
        [SerializeField] private float _playerGravitationForce;
        [SerializeField] [Range(0, 1)] private float _playerLateralMovementOffset;
        
        [Header("Pipe movement")]
        [SerializeField] private float _playerPipeMovementSpeed;
        [SerializeField] private float _playerPipeFallSpeed;
        
        [Header("Ice movement")]
        [SerializeField] private float _playerIceForwardSpeed;
        [SerializeField] private float _playerIceLateralSpeed;
        [SerializeField] [Range(0, 1)] private float _playerIceFriction;
        
        [Header("Guides movement")]
        [SerializeField] private Vector3 _playerGuidesStartPosition;
        [SerializeField] private float _playerGuidesMovementSpeed;
        
        [Header("Environment")]
        [SerializeField] private float _roadWidth;
        
        [Header("Camera")]
        [SerializeField] private float _cameraSmooth;
        [SerializeField] private List<CameraData> _cameraData;

        [Header("Barbell Track")] 
        [SerializeField] private float _barbellVelocity;
        [SerializeField] private List<Color> _finishGroundColors;

        #region IGameConfigProvider

        float IGameConfigProvider.PlayerForwardSpeed => _playerForwardSpeed;
        float IGameConfigProvider.PlayerLateralSpeed => _playerLateralSpeed;
        float IGameConfigProvider.PlayerLimitSpeed => _playerLimitForce;
        float IGameConfigProvider.PlayerGravitationForce => _playerGravitationForce;
        float IGameConfigProvider.PlayerLateralMovementOffset => _playerLateralMovementOffset;
        float IGameConfigProvider.PlayerPipeMovementSpeed => _playerPipeMovementSpeed;
        float IGameConfigProvider.PlayerPipeFallSpeed => _playerPipeFallSpeed;
        float IGameConfigProvider.PlayerIceForwardSpeed => _playerIceForwardSpeed;
        float IGameConfigProvider.PlayerIceLateralSpeed => _playerIceLateralSpeed;
        float IGameConfigProvider.PlayerIceFriction => _playerIceFriction;
        Vector3 IGameConfigProvider.PlayerGuidesStartPosition => _playerGuidesStartPosition;
        float IGameConfigProvider.PlayerGuidesMovementSpeed => _playerGuidesMovementSpeed;
        float IGameConfigProvider.RoadWidth => _roadWidth;
        float IGameConfigProvider.CameraSmooth => _cameraSmooth;
        List<CameraData> IGameConfigProvider.CameraData => _cameraData;
        float IGameConfigProvider.BarbellVelocity => _barbellVelocity;
        List<Color> IGameConfigProvider.FinishGroundColors => _finishGroundColors;

        #endregion
    }
}