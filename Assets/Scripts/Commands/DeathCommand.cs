using System;
using System.Collections.Generic;
using Services;
using Signals;
using UnityEngine.SceneManagement;
using ViewModels;
using Zenject;

namespace Commands
{
    public sealed class DeathCommand : IDisposable
    {
        private IPlayerViewModel _playerViewModel;
        private List<IUpdatableService> _updatableServices;
        private readonly SignalBus _signalBus;

        public DeathCommand(IPlayerViewModel playerViewModel, List<IUpdatableService> updatableServices, SignalBus signalBus)
        {
            _playerViewModel = playerViewModel;
            _updatableServices = updatableServices;
            _signalBus = signalBus;
            _signalBus.Subscribe<DeathSignal>(OnDeath);
        }

        private void OnDeath()
        {
            foreach (var updatableService in _updatableServices)
            {
                updatableService.Pause();
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Dispose()
        {
            _signalBus.Unsubscribe<SwitchMovementSignal>(OnDeath);
        }

        #region IDisposable

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}