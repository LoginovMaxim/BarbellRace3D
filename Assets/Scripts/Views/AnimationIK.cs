using UnityEngine;

namespace Views
{
    public sealed class AnimationIK : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private Transform _leftHandTarget;
        [SerializeField] private Transform _rightHandTarget;

        private void OnAnimatorIK(int layerIndex)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandTarget.position);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandTarget.position);
            
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandTarget.rotation);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandTarget.rotation);
        }
    }
}