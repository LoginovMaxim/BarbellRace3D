using Signals;
using UnityEngine;
using Zenject;

namespace Views
{
    public sealed class BarbellView : MonoBehaviour
    {
        public Transform LeftStackParent => _leftStackParent;
        public Transform RightStackParent => _rightStackParent;
        public Transform BarbellParent => _barbellParent;
        public Rigidbody Rigidbody => _rigidbody;
        
        [Inject] private readonly SignalBus _signalBus;
        
        [SerializeField] private Transform _leftStackParent;
        [SerializeField] private Transform _rightStackParent;
        [SerializeField] private Transform _barbellParent;
        [SerializeField] private Rigidbody _rigidbody;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<FinishWall>(out var finishWall))
            {
                return;
            }
            
            _signalBus.Fire<StopBarbellSignal>();
        }
    }
}