using UnityEngine;
using ViewModels;
using Zenject;

namespace Views
{
    public sealed class GroundChecker : MonoBehaviour
    {
        [Inject] private IPlayerViewModel _playerViewModel;

        private void OnTriggerStay(Collider other)
        {
            var isGrounded = other.gameObject.TryGetComponent<Ground>(out var ground);
            _playerViewModel.IsGrounded = isGrounded;
        }
    }
}