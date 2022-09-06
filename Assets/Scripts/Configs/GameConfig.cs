using Providers;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "BarbellRace3D/GameConfig")]
    public sealed class GameConfig : ScriptableObject, IGameConfigProvider
    {
        [Header("Player")]
        [SerializeField] private float _playerLateralSpeed;
        [SerializeField] private float _playerForwardSpeed;
        [SerializeField] [Range(0, 1)] private float _playerLateralMovementOffset;
        
        [Header("Environment")]
        [SerializeField] private float _roadWidth;
        
        [Header("Camera")]
        [SerializeField] private float _cameraSmooth;
        [SerializeField] private Vector3 _cameraRunOffset;
        
        #region IGameConfigProvider

        float IGameConfigProvider.PlayerLateralSpeed => _playerLateralSpeed;
        float IGameConfigProvider.PlayerForwardSpeed => _playerForwardSpeed;
        float IGameConfigProvider.PlayerLateralMovementOffset => _playerLateralMovementOffset;
        float IGameConfigProvider.RoadWidth => _roadWidth;
        public float CameraSmooth => _cameraSmooth;
        public Vector3 CameraRunOffset => _cameraRunOffset;

        #endregion
    }
}