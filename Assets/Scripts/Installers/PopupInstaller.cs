using App.UI.Commands;
using App.UI.Popups.Logics;
using App.UI.Popups.ViewModels;
using App.UI.Services;
using App.UI.Signals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class PopupInstaller : MonoInstaller
    {
        public Transform PopupParent;
        
        public IconPopupViewModel IconPopupViewModel;
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<ClosePopupSignal>();

            Container.Bind<ShowIconPopupCommand>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<IconPopup>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PopupService>().AsSingle().NonLazy();
            
            Container
                .BindMemoryPool<IconPopupViewModel, IconPopupViewModel.Pool>()
                .FromComponentInNewPrefab(IconPopupViewModel)
                .UnderTransform(PopupParent)
                .AsCached()
                .NonLazy();
        }
    }
}