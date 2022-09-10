using UnityEngine;

namespace Views
{
    public sealed class MovementTrigger : MonoBehaviour
    {
        public MovementType MovementType => _movementType;
        
        [SerializeField] private MovementType _movementType;
    }
}