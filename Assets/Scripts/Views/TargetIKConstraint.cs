using UnityEngine;

namespace Views
{
    public sealed class TargetIKConstraint : MonoBehaviour
    {
        [SerializeField] private TargetIKData[] _targetIKData;

        private void Update()
        {
            foreach (var targetIKData in _targetIKData)
            {
                targetIKData.Align();
            }
        }
    }
}