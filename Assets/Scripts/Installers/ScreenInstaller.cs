using App.UI.Screens.Logics;
using App.UI.Screens.ViewModels;
using App.UI.Services;
using App.UI.Signals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class ScreenInstaller : MonoInstaller
    {
        public Transform ScreenParent;
        
        public MainScreenViewModel MainScreenViewModelPrefab;
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<SwitchScreenSignal>();
            
            Container
                .BindFactory<MainScreenViewModel, MainScreenViewModel.Factory>()
                .FromComponentInNewPrefab(MainScreenViewModelPrefab)
                .UnderTransform(ScreenParent)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<MainScreen>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<ScreenService>().AsSingle().NonLazy();
        }
    }
}