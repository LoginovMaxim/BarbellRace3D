using Providers;
using UnityEngine;
using ViewModels;
using Zenject;

namespace App.Monos
{
    public sealed class CameraFollow : MonoBehaviour
    {
        private Transform _target;
        private IGameConfigProvider _gameConfigProvider;

        [Inject] public void Inject(IPlayerViewModel playerViewModel, IGameConfigProvider gameConfigProvider)
        {
            _target = playerViewModel.Transform;
            _gameConfigProvider = gameConfigProvider;
        }

        private void LateUpdate()
        {
            var targetPosition = _target.position + _gameConfigProvider.CameraRunOffset;
            var pursuitSmooth = _gameConfigProvider.CameraSmooth * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, pursuitSmooth);
        }
    }
}