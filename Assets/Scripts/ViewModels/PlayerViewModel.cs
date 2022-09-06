using System;
using App.UI;
using UnityEngine;
using UnityWeld.Binding;
using Zenject;

namespace ViewModels
{
    [Binding] public sealed class PlayerViewModel : ViewModel, IPlayerViewModel
    {
        private static readonly int IsRunAnimator = Animator.StringToHash("IsRun");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _leftHandPivot;
        [SerializeField] private Transform _rightHandPivot;
        [SerializeField] private Transform _leftHandPivotTarget;
        [SerializeField] private Transform _rightHandPivotTarget;

        private bool _isRun;
        
        [Inject] public void Inject(Vector3 position)
        {
            transform.transform.position = position;
        }

        private void Update()
        {
            _leftHandPivot.position = _leftHandPivotTarget.position;
            _leftHandPivot.rotation = _leftHandPivotTarget.rotation;
            _rightHandPivot.position = _rightHandPivotTarget.position;
            _rightHandPivot.rotation = _rightHandPivotTarget.rotation;
        }

        private void SetAnimatorRun(bool value)
        {
            _animator.SetBool(IsRunAnimator, value);
        }

        #region IPlayerViewModel

        Transform IPlayerViewModel.Transform => transform;

        bool IPlayerViewModel.IsRun
        {
            get
            {
                return _isRun;
            }
            set
            {
                if (_isRun == value)
                {
                    return;
                }

                _isRun = value;
                SetAnimatorRun(_isRun);
            }
        }

        #endregion

        public sealed class Factory : PlaceholderFactory<Vector3, PlayerViewModel>
        {
        }
    }
}