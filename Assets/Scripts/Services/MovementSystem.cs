using Monos;
using Providers;
using UnityEngine;
using ViewModels;
using Views;

namespace Services
{
    public abstract class MovementSystem : UpdatableService, IMovement
    {
        public abstract MovementType MovementType { get; }
        public bool IsEnabled => !IsPause;

        protected readonly IInputService InputService;
        protected readonly IGameConfigProvider GameConfigProvider;
        protected readonly IPlayerViewModel PlayerViewModel;
        protected readonly ICameraFollow CameraFollow;
        protected readonly BarbellView BarbellView;

        public MovementSystem(
            IInputService inputService,
            IGameConfigProvider gameConfigProvider,
            IPlayerViewModel playerViewModel,
            ICameraFollow cameraFollow,
            BarbellView barbellView,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(monoUpdater, updateType, isImmediateStart)
        {
            InputService = inputService;
            GameConfigProvider = gameConfigProvider;
            PlayerViewModel = playerViewModel;
            CameraFollow = cameraFollow;
            BarbellView = barbellView;

            InputService.InputStarted += OnInputStarted;
            InputService.InputEnded += OnInputEnded;
        }

        public void Enable()
        {
            UnPause();
            OnEnabled();
        }

        public void Disable()
        {
            Pause();
            OnDisabled();
        }

        protected abstract void OnEnabled();
        protected abstract void OnDisabled();

        protected abstract void OnInputStarted();
        protected abstract void OnInputEnded();

        protected float GetWeightCoefficient()
        {
            var coefficient = PlayerViewModel.DiskCount / 40;
            return 1 + coefficient;
        }

        protected override void Dispose()
        {
            InputService.InputStarted -= OnInputStarted;
            InputService.InputEnded -= OnInputEnded;
        }
    }
}