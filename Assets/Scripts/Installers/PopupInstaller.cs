using UI.Commands;
using UI.Popups.Logics;
using UI.Popups.ViewModels;
using UI.Services;
using UI.Signals;
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