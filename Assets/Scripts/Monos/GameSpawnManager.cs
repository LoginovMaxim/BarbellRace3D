using UnityEngine;

namespace App.Monos
{
    public sealed class GameSpawnManager : MonoBehaviour, IGameSpawnManager
    {
        [SerializeField] private Transform _playerSpawnPosition;

        #region IGameSpawnManager

        Vector3 IGameSpawnManager.PlayerSpawnPosition => _playerSpawnPosition.position;

        #endregion
    }
}