using UnityEngine;
using UnityWeld.Binding;
using Zenject;

namespace UI.Popups.ViewModels
{
    [Binding] public sealed class IconPopupViewModel : PopupViewModel
    {
        
        [Binding] public Sprite Sprite
        {
            get
            {
                return _sprite;
            }
            set
            {
                if (_sprite == value)
                {
                    return;
                }

                _sprite = value;
                OnPropertyChanged(nameof(Sprite));
            }
        }

        private Sprite _sprite;
        
        private void SetSprite(Sprite sprite)
        {
            Sprite = sprite;
        }

        public sealed class Pool : MemoryPool<IconPopupData, IconPopupViewModel>
        {
            protected override void OnSpawned(IconPopupViewModel iconPopupViewModel)
            {
                iconPopupViewModel.SetActive(true);
            }

            protected override void Reinitialize(IconPopupData iconPopupData, IconPopupViewModel iconPopupViewModel)
            {
                iconPopupViewModel.SetIndex(iconPopupData.Index);
                iconPopupViewModel.SetLabel(iconPopupData.Label);
                iconPopupViewModel.SetDescription(iconPopupData.Description);
                iconPopupViewModel.SetSprite(iconPopupData.Sprite);
            }

            protected override void OnDespawned(IconPopupViewModel iconPopupViewModel)
            {
                iconPopupViewModel.SetActive(false);
            }
        }
    }
}