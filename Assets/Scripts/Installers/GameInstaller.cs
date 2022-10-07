using System.Collections.Generic;
using Assemblers;
using Assemblers.Parts;
using Commands;
using Monos;
using Services;
using Signals;
using UnityEngine;
using Utils;
using ViewModels;
using Views;
using Zenject;

namespace Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        public PlayerViewModel PlayerViewModel;
        
        public override void InstallBindings()
        {
            // signals
            Container.DeclareSignal<TakeDiskSignal>();
            Container.DeclareSignal<SwitchMovementSignal>();
            Container.DeclareSignal<DeathSignal>();
            
            // scene monos
            Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameSpawnManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            if (FindObjectOfType<GuidesView>())
            {
                Container.Bind<GuidesView>().FromComponentInHierarchy().AsSingle().NonLazy();
            }

            // factories
            Container.BindFactory<Vector3, PlayerViewModel, PlayerViewModel.Factory>().FromComponentInNewPrefab(PlayerViewModel).AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerViewModel>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // scene monos needed factories
            Container.BindInterfacesTo<CameraFollow>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // services
            Container.BindService<InputService>(UpdateType.Update, true);
            Container.BindService<CommonMovementSystem>(UpdateType.FixedUpdate, true);
            Container.BindService<PipeMovementSystem>(UpdateType.Update);
            Container.BindService<IceMovementSystem>(UpdateType.Update);
            Container.BindService<GuidesMovementSystem>(UpdateType.Update);

            // assembler parts
            Container.Bind<GameBuilder>().AsSingle().NonLazy();
            
            // views
            //Container.Bind<GroundChecker>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // commands
            Container.Bind<TakeDiskCommand>().AsSingle().NonLazy();
            Container.Bind<SwitchMovementCommand>().AsSingle().NonLazy();
            Container.Bind<DeathCommand>().AsSingle().NonLazy();

            // assembler
            Container.BindAssembler<GameAssembler>(new List<IAssemblerPart>
            {
                Container.Resolve<GameBuilder>(),
            });
        }
    }
}