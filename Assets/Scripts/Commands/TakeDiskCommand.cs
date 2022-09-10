using System;
using Signals;
using ViewModels;
using Views;
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
            _signalBus.Subscribe<TakeDiskSignal>(x => OnTakeDisk(x.Disk));

            _playerViewModel = playerViewModel;
        }

        private void OnTakeDisk(Disk disk)
        {
            if (disk.transform.position.x < 0)
            {
                _playerViewModel.LeftDiskCount += disk.DiskCount;
            }
            else
            {
                _playerViewModel.RightDiskCount += disk.DiskCount;
            }
        }
        
        private void Dispose()
        {
            _signalBus.Unsubscribe<TakeDiskSignal>(x => OnTakeDisk(x.Disk));
        }

        #region IDisposable

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}