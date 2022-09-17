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
        [SerializeField] [Range(0, 1)] private float _playerLateralMovementOffset;
        
        [Header("Pipe movement")]
        [SerializeField] private float _playerPipeMovementSpeed;
        [SerializeField] private float _playerPipeFallSpeed;
        
        [Header("Ice movement")]
        [SerializeField] private float _playerIceForwardSpeed;
        [SerializeField] private float _playerIceLateralSpeed;
        [SerializeField] [Range(0, 1)] private float _playerIceFriction;
        
        [Header("Environment")]
        [SerializeField] private float _roadWidth;
        
        [Header("Camera")]
        [SerializeField] private float _cameraSmooth;
        [SerializeField] private Vector3 _cameraRunOffset;

        #region IGameConfigProvider

        float IGameConfigProvider.PlayerForwardSpeed => _playerForwardSpeed;
        float IGameConfigProvider.PlayerLateralSpeed => _playerLateralSpeed;
        float IGameConfigProvider.PlayerLateralMovementOffset => _playerLateralMovementOffset;
        float IGameConfigProvider.PlayerPipeMovementSpeed => _playerPipeMovementSpeed;
        float IGameConfigProvider.PlayerPipeFallSpeed => _playerPipeFallSpeed;
        float IGameConfigProvider.PlayerIceForwardSpeed => _playerIceForwardSpeed;
        float IGameConfigProvider.PlayerIceLateralSpeed => _playerIceLateralSpeed;
        float IGameConfigProvider.PlayerIceFriction => _playerIceFriction;
        float IGameConfigProvider.RoadWidth => _roadWidth;
        public float CameraSmooth => _cameraSmooth;
        public Vector3 CameraRunOffset => _cameraRunOffset;

        #endregion
    }
}