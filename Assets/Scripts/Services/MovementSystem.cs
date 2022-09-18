using System;
using App.Monos;
using Providers;
using UnityEngine;
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

            InputService.InputStarted += OnInputStarted;
            InputService.InputEnded += OnInputEnded;
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

        protected abstract void OnInputStarted();
        protected abstract void OnInputEnded();

        protected float GetWeightCoefficient()
        {
            var coefficient = 1f;
            return coefficient;
            
            if (PlayerViewModel.LeftDiskCount > PlayerViewModel.RightDiskCount)
            {
                coefficient = Mathf.Max((float) PlayerViewModel.RightDiskCount / PlayerViewModel.LeftDiskCount, 0.1f);
                coefficient = 1 - coefficient;

                if (InputService.Horizontal < 0)
                {
                    coefficient = 1 + coefficient;
                }
                else if (InputService.Horizontal > 0)
                {
                    coefficient = 1 - coefficient;
                }
            }
            else if (PlayerViewModel.RightDiskCount > PlayerViewModel.LeftDiskCount)
            {
                coefficient = Mathf.Max((float) PlayerViewModel.LeftDiskCount / PlayerViewModel.RightDiskCount, 0.1f);
                coefficient = 1 - coefficient;
                
                if (InputService.Horizontal > 0)
                {
                    coefficient = 1 + coefficient;
                }
                else if (InputService.Horizontal < 0)
                {
                    coefficient = 1 - coefficient;
                }
            }

            return coefficient / 1.5f + 0.33f;
        }

        protected override void Dispose()
        {
            InputService.InputStarted -= OnInputStarted;
            InputService.InputEnded -= OnInputEnded;
        }
    }
}