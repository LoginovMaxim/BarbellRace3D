using Configs;
using Zenject;

namespace Installers
{
    public sealed class ConfigInstaller : MonoInstaller
    {
        public GameConfig GameConfig;
        public MapSchemeConfig MapSchemeConfig;
        public RoadPrefabsConfig roadPrefabsConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameConfig>().FromScriptableObject(GameConfig).AsSingle().NonLazy();
            Container.BindInterfacesTo<MapSchemeConfig>().FromScriptableObject(MapSchemeConfig).AsSingle().NonLazy();
            Container.BindInterfacesTo<RoadPrefabsConfig>().FromScriptableObject(roadPrefabsConfig).AsSingle().NonLazy();
        }
    }
}