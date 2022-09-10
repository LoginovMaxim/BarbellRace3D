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

        private SignalBus _signalBus;
        private bool _isRun;
        private int _diskCount;
        
        [Inject] public void Inject(Vector3 position, SignalBus signalBus)
        {
            transform.transform.position = position;
            _signalBus = signalBus;
        }

        private void SetAnimatorRun(bool value)
        {
            _animator.SetBool(IsRunAnimator, value);
        }

        private void SetViewStack()
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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Disk>(out var disk))
            {
                _signalBus.Fire(new TakeDiskSignal(disk.DiskCount));
                Destroy(disk.gameObject);
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

        int IPlayerViewModel.DiskCount
        {
            get { return _diskCount; }
            set
            {
                if (_diskCount == value)
                {
                    return;
                }

                _diskCount = value;
                SetViewStack();
            }
        }

        #endregion

        public sealed class Factory : PlaceholderFactory<Vector3, PlayerViewModel>
        {
        }
    }
}