using System.Collections.Generic;
using App.Assemblers;
using App.Assemblers.Parts;
using App.Monos;
using App.Services;
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
            // scene monos
            Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameSpawnManager>().FromComponentInHierarchy().AsSingle().NonLazy();

            // factories
            Container.BindFactory<Vector3, PlayerViewModel, PlayerViewModel.Factory>().FromComponentInNewPrefab(PlayerViewModel).AsSingle().NonLazy();
            Container.Bind<PlayerViewModel>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // scene monos needed factories
            Container.Bind<CameraFollow>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            // services
            Container.BindService<InputService>(UpdateType.Update, true);
            Container.BindService<MovementService>(UpdateType.Update, true);

            // assembler parts
            Container.Bind<GameBuilder>().AsSingle().NonLazy();

            // assembler
            Container.BindAssembler<GameAssembler>(new List<IAssemblerPart>
            {
                Container.Resolve<GameBuilder>(),
            });
        }
    }
}