using UI.Screens.Logics;
using UI.Screens.ViewModels;
using UI.Services;
using UI.Signals;
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