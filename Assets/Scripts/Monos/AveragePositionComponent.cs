using UnityEngine;

namespace Monos
{
    public sealed class AveragePositionComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] _targetPositions;

        private void Update()
        {
            var averagePosition = Vector3.zero;
            foreach (var targetPosition in _targetPositions)
            {
                averagePosition += targetPosition.position;
            }

            averagePosition /= _targetPositions.Length;
            transform.position = averagePosition;
        }
    }
}