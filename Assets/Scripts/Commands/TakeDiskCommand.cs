using System;
using Signals;
using ViewModels;
using Zenject;

namespace Commands
{
    public sealed class TakeDiskCommand : IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IPlayerViewModel _playerViewModel;

        public TakeDiskCommand(SignalBus signalBus, IPlayerViewModel playerViewModel)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<TakeDiskSignal>(x => OnTakeDisk(x.DiskCount));

            _playerViewModel = playerViewModel;
        }

        private void OnTakeDisk(int diskCount)
        {
            _playerViewModel.DiskCount += diskCount;
        }
        
        private void Dispose()
        {
            _signalBus.Unsubscribe<TakeDiskSignal>(x => OnTakeDisk(x.DiskCount));
        }

        #region IDisposable

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}