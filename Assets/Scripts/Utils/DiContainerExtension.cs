using System.Collections.Generic;
using App.Assemblers;
using App.Services;
using Zenject;

namespace Utils
{
    public static class DiContainerExtension
    {
        public static IfNotBoundBinder BindService<TUpdatableService>(this DiContainer container, UpdateType updateType, bool isImmediateStart = false)
            where TUpdatableService : IUpdatableService
        {
            return container.Bind<TUpdatableService>().AsSingle().WithArguments(updateType, isImmediateStart).NonLazy();
        }
        
        public static IfNotBoundBinder BindAssembler<TAssembler>(this DiContainer container, List<IAssemblerPart> assemblerPart)
            where TAssembler : Assembler
        {
            return container.BindInterfacesAndSelfTo<TAssembler>().AsSingle().WithArguments(assemblerPart).NonLazy();
        }
    }
}