using System.Collections.Generic;
using App.Assemblers;
using App.Assemblers.Parts;
using App.Monos;
using App.Services;
using Commands;
using Signals;
using UnityEngine;
using Utils;
using ViewModels;
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
            
            // scene monos
            Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameSpawnManager>().FromComponentInHierarchy().AsSingle().NonLazy();

            // factories
            Container.BindFactory<Vector3, PlayerViewModel, PlayerViewModel.Factory>().FromComponentInNewPrefab(PlayerViewModel).AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerViewModel>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // scene monos needed factories
            Container.Bind<CameraFollow>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // services
            Container.BindService<InputService>(UpdateType.Update, true);
            Container.BindService<CommonMovementSystem>(UpdateType.Update, true);
            Container.BindService<PipeMovementSystem>(UpdateType.Update);
            Container.BindService<IceMovementSystem>(UpdateType.Update);

            // assembler parts
            Container.Bind<GameBuilder>().AsSingle().NonLazy();
            
            // commands
            Container.Bind<TakeDiskCommand>().AsSingle().NonLazy();
            Container.Bind<SwitchMovementCommand>().AsSingle().NonLazy();

            // assembler
            Container.BindAssembler<GameAssembler>(new List<IAssemblerPart>
            {
                Container.Resolve<GameBuilder>(),
            });
        }
    }
}