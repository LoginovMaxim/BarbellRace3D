using App.UI;
using UnityEngine;
using UnityWeld.Binding;
using Zenject;

namespace ViewModels
{
    [Binding]
    public sealed class PlayerViewModel : ViewModel
    {
        [Inject] public void Inject(Vector3 position)
        {
            transform.transform.position = position;
        }

        public sealed class Factory : PlaceholderFactory<Vector3, PlayerViewModel>
        {
        }
    }
}