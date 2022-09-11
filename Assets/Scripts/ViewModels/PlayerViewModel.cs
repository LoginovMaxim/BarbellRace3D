using System;
using System.Collections.Generic;
using App.UI;
using Signals;
using UnityEngine;
using UnityWeld.Binding;
using Views;
using Zenject;

namespace ViewModels
{
    [Binding] public sealed class PlayerViewModel : ViewModel, IPlayerViewModel
    {
        private static readonly int IsRunAnimator = Animator.StringToHash("IsRun");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _leftStackParent;
        [SerializeField] private Transform _rightStackParent;
        [SerializeField] private Transform _barbellParent;
        [SerializeField] private Transform _tubeRoundParent;

        [SerializeField] private List<Vector3> _movementBuffer;

        public List<Vector3> MovementBuffer
        {
            get => _movementBuffer;
            set => _movementBuffer = value;
        }

        private SignalBus _signalBus;
        private bool _isRun;
        private int _leftDiskCount;
        private int _rightDiskCount;
        
        [Inject] public void Inject(Vector3 position, SignalBus signalBus)
        {
            transform.transform.position = position;
            _signalBus = signalBus;
        }

        private void SetAnimatorRun(bool value)
        {
            _animator.SetBool(IsRunAnimator, value);
        }

        private void SetViewLeftStack()
        {
            if (_leftDiskCount > _leftStackParent.childCount)
            {
                _leftDiskCount = _leftStackParent.childCount;
            }
            
            var childIndex = 0;
            foreach (Transform child in _leftStackParent)
            {
                child.gameObject.SetActive(childIndex == _leftDiskCount - 1);
                childIndex++;
            }
        }

        private void SetViewRightStack()
        {
            if (_rightDiskCount > _rightStackParent.childCount)
            {
                _rightDiskCount = _rightStackParent.childCount;
            }
            
            var childIndex = 0;
            foreach (Transform child in _rightStackParent)
            {
                child.gameObject.SetActive(childIndex == _rightDiskCount - 1);
                childIndex++;
            }
        }

        private void Update()
        {
            SetBarbellRotation();
        }

        private void SetBarbellRotation()
        {
            var differentDiskCount = _leftDiskCount - _rightDiskCount;

            var rotation = _barbellParent.localRotation.eulerAngles;
            rotation.z = differentDiskCount * 2;

            _barbellParent.localRotation = Quaternion.Lerp(_barbellParent.localRotation,Quaternion.Euler(rotation), Time.deltaTime * 10);
        }

        /*private void SetViewStack()
        {
            var isLeftSide = _diskCount % 2 == 0;
            var stackParent = isLeftSide ? _leftStackParent : _rightStackParent;
            var diskCount = isLeftSide ? _diskCount : _diskCount + 1;
            diskCount /= 2;
            diskCount -= 1;

            if (diskCount > stackParent.childCount - 1)
            {
                diskCount = stackParent.childCount - 1;
            }
            
            var childIndex = 0;
            foreach (Transform child in stackParent)
            {
                child.gameObject.SetActive(childIndex == diskCount);
                childIndex++;
            }
        }*/

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Disk>(out var disk))
            {
                _signalBus.Fire(new TakeDiskSignal(disk));
                Destroy(disk.gameObject);
            }
            
            if (other.gameObject.TryGetComponent<MovementTrigger>(out var movementTrigger))
            {
                _signalBus.Fire(new SwitchMovementSignal(movementTrigger.MovementType));
            }
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

        int IPlayerViewModel.LeftDiskCount
        {
            get
            {
                return _leftDiskCount;
            }
            set
            {
                if (_leftDiskCount == value)
                {
                    return;
                }

                _leftDiskCount = value;
                SetViewLeftStack();
            }
        }

        int IPlayerViewModel.RightDiskCount
        {
            get { return _rightDiskCount; }
            set
            {
                if (_rightDiskCount == value)
                {
                    return;
                }

                _rightDiskCount = value;
                SetViewRightStack();
            }
        }

        Transform IPlayerViewModel.TubeRoundParent => _tubeRoundParent;

        #endregion

        public sealed class Factory : PlaceholderFactory<Vector3, PlayerViewModel>
        {
        }
    }
}