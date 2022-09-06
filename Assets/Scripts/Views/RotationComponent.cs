using UnityEngine;

namespace Views
{
    public sealed class RotationComponent : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        
        private void Update()
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        }
    }
}