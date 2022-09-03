using System.Collections.Generic;
using App.UI.Popups.ViewModels;
using App.UI.Signals;
using Zenject;

namespace App.UI.Popups.Logics
{
    public sealed class IconPopup : IIconPopup
    {
        private readonly IconPopupViewModel.Pool _pool;
        
        private Dictionary<int, IconPopupViewModel> _iconPopups = new Dictionary<int, IconPopupViewModel>();
        
        private static int _popupIndex;
        
        public IconPopup(IconPopupViewModel.Pool pool, SignalBus signalBus)
        {
            _pool = pool;
            signalBus.Subscribe<ClosePopupSignal>(x => Despawn(x.PopupIndex));
        }

        private void Spawn(IconPopupData iconPopupData)
        {
            iconPopupData.Index = ++_popupIndex;
            var viewModel = _pool.Spawn(iconPopupData);
            _iconPopups.Add(_popupIndex, viewModel);
        }

        private void Despawn(int index)
        {
            if (!_iconPopups.ContainsKey(index))
            {
                return;
            }
            
            _pool.Despawn(_iconPopups[index]);
            _iconPopups.Remove(index);
        }
        
        #region IPopup
        
        bool IPopup.Spawned => _iconPopups.Count > 0;

        void IPopup.Despawn(int index)
        {
            Despawn(index);
        }

        #endregion
        
        #region IIconPopup
        
        void IIconPopup.Spawn(IconPopupData iconPopupData)
        {
            Spawn(iconPopupData);
        }

        #endregion
    }
}