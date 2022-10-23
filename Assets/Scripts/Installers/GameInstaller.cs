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
        public bool IsBarbellFinishGame;
        public PlayerViewModel PlayerViewModel;
        
        public override void InstallBindings()
        {
            // signals
            Container.DeclareSignal<TakeDiskSignal>();
            Container.DeclareSignal<SwitchMovementSignal>();
            Container.DeclareSignal<DeathSignal>();
            Container.DeclareSignal<FinishBarbellTrackSignal>();
            Container.DeclareSignal<ThrowBarbellSignal>();
            Container.DeclareSignal<StopBarbellSignal>();
            Container.DeclareSignal<SetRailViewSignal>();

            // factories
            Container.BindFactory<Vector3, PlayerViewModel, PlayerViewModel.Factory>().FromComponentInNewPrefab(PlayerViewModel).AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerViewModel>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // scene monos
            Container.Bind<InputReceiver>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<BarbellView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameSpawnManager>().FromComponentInHierarchy().AsSingle().NonLazy();

            // assembler parts
            Container.Bind<GameBuilder>().AsSingle().NonLazy();
            Container.Bind<MapBuilder>().AsSingle().NonLazy();

            // assembler
            Container.BindAssembler<GameAssembler>(new List<IAssemblerPart>
            {
                Container.Resolve<GameBuilder>(),
                Container.Resolve<MapBuilder>(),
            });
            
            if (IsBarbellFinishGame)
            {
                Container.BindInterfacesTo<BarbellTrackViewModel>().FromComponentInHierarchy().AsSingle().NonLazy();
            }
            
            // scene monos needed factories
            Container.BindInterfacesTo<CameraFollow>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // services
            Container.BindService<InputService>(UpdateType.Update, true);
            Container.BindService<CommonMovementSystem>(UpdateType.FixedUpdate, true);
            Container.BindService<PipeMovementSystem>(UpdateType.Update);
            Container.BindService<IceMovementSystem>(UpdateType.Update);
            Container.BindService<RailMovementSystem>(UpdateType.Update);

            if (IsBarbellFinishGame)
            {
                Container.BindService<BarbellMovementSystem>(UpdateType.Update | UpdateType.FixedUpdate, true);
            }
            
            // commands
            Container.Bind<TakeDiskCommand>().AsSingle().NonLazy();
            Container.Bind<SwitchMovementCommand>().AsSingle().NonLazy();
            Container.Bind<DeathCommand>().AsSingle().NonLazy();
            Container.Bind<FinishBarbellTrackCommand>().AsSingle().NonLazy();
            Container.Bind<ThrowBarbellCommand>().AsSingle().NonLazy();
            Container.Bind<StopBarbellCommand>().AsSingle().NonLazy();
            Container.Bind<SetRailViewCommand>().AsSingle().NonLazy();
        }
    }
}