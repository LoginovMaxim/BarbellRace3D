using App.Monos;
using Providers;
using ViewModels;
using Views;

namespace App.Services
{
    public abstract class MovementSystem : UpdatableService, IMovement
    {
        public abstract MovementType MovementType { get; }
        
        protected readonly IInputService InputService;
        protected readonly IGameConfigProvider GameConfigProvider;
        protected readonly IPlayerViewModel PlayerViewModel;
        
        public MovementSystem(
            IInputService inputService,
            IGameConfigProvider gameConfigProvider,
            IPlayerViewModel playerViewModel,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(monoUpdater, updateType, isImmediateStart)
        {
            InputService = inputService;
            GameConfigProvider = gameConfigProvider;
            PlayerViewModel = playerViewModel;
        }

        public void Enable()
        {
            Pause();
            OnPaused();
        }

        public void Disable()
        {
            UnPause();
            OnUnPaused();
        }

        protected abstract void OnPaused();
        protected abstract void OnUnPaused();
    }
}