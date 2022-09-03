using Configs;
using Zenject;

namespace Installers
{
    public sealed class ConfigInstaller : MonoInstaller
    {
        public GameConfig GameConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameConfig>().FromScriptableObject(GameConfig).AsSingle().NonLazy();
        }
    }
}