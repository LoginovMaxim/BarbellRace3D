using System;
using Signals;
using UnityEngine;
using ViewModels;
using Views;
using Zenject;

namespace Commands
{
    public sealed class TakeDiskCommand : IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IPlayerViewModel _playerViewModel;
        private readonly BarbellView _barbellView;
        
        public TakeDiskCommand(SignalBus signalBus, IPlayerViewModel playerViewModel, BarbellView barbellView)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<TakeDiskSignal>(x => OnTakeDisk(x.Disk));

            _playerViewModel = playerViewModel;
            _barbellView = barbellView;
        }

        private void OnTakeDisk(Disk disk)
        {
            for (var i = 0; i < disk.DiskCount; i++)
            {
                _playerViewModel.DiskCount++;
                SetViewStack();
            }
        }

        private void SetViewStack()
        {
            var isLeftSide = _playerViewModel.DiskCount % 2 == 0;
            var stackParent = isLeftSide ? _barbellView.LeftStackParent : _barbellView.RightStackParent;
            var diskCount = isLeftSide ? _playerViewModel.DiskCount : _playerViewModel.DiskCount + 1;
            diskCount /= 2;
            diskCount -= 1;

            if (diskCount > stackParent.childCount - 1)
            {
                diskCount = stackParent.childCount - 1;
            }
            
            var childIndex = 0;
            foreach (Transform child in stackParent)
            {
                child.gameObject.SetActive(childIndex == diskCount);
                childIndex++;
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