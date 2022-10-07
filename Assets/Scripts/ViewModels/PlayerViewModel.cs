using Monos;
using Signals;
using UI;
using UnityEngine;
using UnityWeld.Binding;
using Utils;
using Views;
using Zenject;

namespace ViewModels
{
    [Binding] public sealed class PlayerViewModel : ViewModel, IPlayerViewModel
    {
        private static readonly int IsRunAnimator = Animator.StringToHash("IsRun");
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _leftStackParent;
        [SerializeField] private Transform _rightStackParent;
        [SerializeField] private Transform _barbellParent;
        [SerializeField] private Transform _tubeRoundParent;

        private SignalBus _signalBus;
        private bool _isRun;
        private bool _isGrounded;
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
            if (other.gameObject.TryGetComponentInParent<Disk>(out var disk))
            {
                _signalBus.Fire(new TakeDiskSignal(disk));
                Destroy(disk.gameObject);
            }
            
            if (other.gameObject.TryGetComponent<MovementTrigger>(out var movementTrigger))
            {
                _signalBus.Fire(new SwitchMovementSignal(movementTrigger.MovementType));
            }
            
            if (other.gameObject.TryGetComponent<DeathTrigger>(out var deathTrigger))
            {
                _signalBus.Fire(new DeathSignal());
            }
        }

        #region IPlayerViewModel

        Transform IPlayerViewModel.Transform => transform;
        Rigidbody IPlayerViewModel.Rigidbody => _rigidbody;

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

        bool IPlayerViewModel.IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }

        int IPlayerViewModel.DiskCount
        {
            get
            {
                return _diskCount;
            }
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

        Transform IPlayerViewModel.TubeRoundParent => _tubeRoundParent;

        Transform IPlayerViewModel.BarbellParent => _barbellParent;

        #endregion

        public sealed class Factory : PlaceholderFactory<Vector3, PlayerViewModel>
        {
        }
    }
}