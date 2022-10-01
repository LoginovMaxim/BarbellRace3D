using UnityEngine;

namespace Monos
{
    public class GameObjectFinder : MonoBehaviour, IGameObjectFinder
    {
        private T GetGameObject<T>() where T : MonoBehaviour
        {
            return FindObjectOfType<T>(true);
        }
        
        private T[] GetGameObjects<T>() where T : MonoBehaviour
        {
            return FindObjectsOfType<T>(true);
        }

        #region IGameObjectFinder

        T IGameObjectFinder.GetGameObject<T>()
        {
            return GetGameObject<T>();
        }
        
        T[] IGameObjectFinder.GetGameObjects<T>()
        {
            return GetGameObjects<T>();
        }

        #endregion
    }
}