using System;
using Zenject;

namespace Commands
{
    public abstract class Command : IDisposable
    {
        protected readonly SignalBus SignalBus;

        protected Command(SignalBus signalBus)
        {
            SignalBus = signalBus;
            Subscribe();
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        #region IDisposable
        
        void IDisposable.Dispose()
        {
            Unsubscribe();
        }

        #endregion
    }
}