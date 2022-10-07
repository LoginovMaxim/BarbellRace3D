using UnityEngine;

namespace Utils
{
    public static class GameObjectExtension
    {
        public static bool TryGetComponentInParent<TComponent>(this GameObject gameObject, out TComponent component) 
            where TComponent : MonoBehaviour
        {
            component = gameObject.GetComponentInParent<TComponent>();
            return component != null;
        }
    }
}