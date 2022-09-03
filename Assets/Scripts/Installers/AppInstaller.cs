using App.Data;
using App.Localizations;
using App.Monos;
using UnityEngine;
using VisualEffects;
using Zenject;

namespace Installers
{
    public sealed class AppInstaller : MonoInstaller
    {
        [SerializeField] private MonoUpdater MonoUpdater;
        [SerializeField] private SceneLoader SceneLoader;
        [SerializeField] private GameObjectFinder GameObjectFinder;
    
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.BindInterfacesTo<MonoUpdater>().FromComponentInNewPrefab(MonoUpdater).AsSingle().NonLazy();
            Container.BindInterfacesTo<SceneLoader>().FromComponentInNewPrefab(SceneLoader).AsSingle().NonLazy();
            Container.BindInterfacesTo<GameObjectFinder>().FromComponentInNewPrefab(GameObjectFinder).AsSingle().NonLazy();
            Container.BindInterfacesTo<EffectEmitter>().AsSingle().NonLazy();
            
            BindData();
            
            Container.Bind<ILocalizationDataProvider>().To<LocalizationDataProvider>().AsSingle().NonLazy();
        }

        private void BindData()
        {
            Container.BindInterfacesTo<DataStorage<PlayerData>>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DataStorage<PlayerCurrency>>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<PlayerProfile>().AsSingle().NonLazy();
        }
    }
}