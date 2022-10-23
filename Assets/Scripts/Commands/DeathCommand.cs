using System;
using System.Collections.Generic;
using Services;
using Signals;
using UnityEngine.SceneManagement;
using ViewModels;
using Zenject;

namespace Commands
{
    public sealed class DeathCommand : Command
    {
        private IPlayerViewModel _playerViewModel;
        private List<IUpdatableService> _updatableServices;

        public DeathCommand(
            IPlayerViewModel playerViewModel, 
            List<IUpdatableService> updatableServices, 
            SignalBus signalBus) : 
            base(signalBus)
        {
            _playerViewModel = playerViewModel;
            _updatableServices = updatableServices;
        }

        protected override void Subscribe()
        {
            SignalBus.Subscribe<DeathSignal>(OnDeath);
        }

        protected override void Unsubscribe()
        {
            SignalBus.Unsubscribe<SwitchMovementSignal>(OnDeath);
        }

        private void OnDeath()
        {
            foreach (var updatableService in _updatableServices)
            {
                updatableService.Pause();
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}