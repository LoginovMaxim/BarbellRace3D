using Monos;
using Services;
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
        private static readonly int IsThrowAnimator = Animator.StringToHash("Throw");
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _tubeRoundParent;
        [SerializeField] private Transform _neckTransform;
        [SerializeField] private Transform _averageHandsTransform;

        private SignalBus _signalBus;
        private bool _isRun;
        private bool _isThrow;
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

        private void SetAnimatorThrow()
        {
            _animator.SetTrigger(IsThrowAnimator);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponentInParent<Disk>(out var disk))
            {
                _signalBus.Fire(new TakeDiskSignal(disk));
                Destroy(disk.gameObject);
            }
            
            if (other.gameObject.TryGetComponent<RailTrigger>(out var guidesTrigger))
            {
                _signalBus.Fire(new SetRailViewSignal(guidesTrigger.RailView));
            }
            
            if (other.gameObject.TryGetComponent<MovementTrigger>(out var movementTrigger))
            {
                _signalBus.Fire(new SwitchMovementSignal(movementTrigger.MovementType));
            }
            
            if (other.gameObject.TryGetComponent<DeathTrigger>(out var deathTrigger))
            {
                _signalBus.Fire(new DeathSignal());
            }
            
            if (other.gameObject.TryGetComponent<FinishBarbellTrackTrigger>(out var finishTrigger))
            {
                _signalBus.Fire(new SwitchMovementSignal(MovementType.None));
                _signalBus.Fire(new FinishBarbellTrackSignal());
            }
        }

        // animation callback
        public void OnThrowBarbell()
        {
            _signalBus.Fire<ThrowBarbellSignal>();
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

        bool IPlayerViewModel.IsThrow
        {
            get
            {
                return _isThrow;
            }
            set
            {
                SetAnimatorThrow();
                if (_isThrow == value)
                {
                    return;
                }

                _isThrow = value;
            }
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
            }
        }

        Transform IPlayerViewModel.NeckTransform => _neckTransform;
        Transform IPlayerViewModel.AverageHandsTransform => _averageHandsTransform;
        Transform IPlayerViewModel.TubeRoundParent => _tubeRoundParent;

        #endregion

        public sealed class Factory : PlaceholderFactory<Vector3, PlayerViewModel>
        {
        }
    }
}