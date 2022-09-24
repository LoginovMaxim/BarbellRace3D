using System;
using App.Monos;

namespace App.Services
{
    public abstract class UpdatableService : IUpdatableService, IDisposable
    {
        private readonly IMonoUpdater _monoUpdater;
        protected bool IsPause = true;
        
        protected UpdatableService(IMonoUpdater monoUpdater, UpdateType updateType, bool isImmediateStart)
        {
            _monoUpdater = monoUpdater;
            
            if (updateType.HasFlag(UpdateType.Update))
            {
                _monoUpdater.Subscribe(UpdateType.Update, OnUpdate);
            }
            if (updateType.HasFlag(UpdateType.FixedUpdate))
            {
                _monoUpdater.Subscribe(UpdateType.FixedUpdate, OnFixedUpdate);
            }
            if (updateType.HasFlag(UpdateType.LateUpdate))
            {
                _monoUpdater.Subscribe(UpdateType.LateUpdate, OnLateUpdate);
            }

            if (isImmediateStart)
            {
                Start();
            }
        }

        protected void Start()
        {
            UnPause();    
        }
        
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }

        private void OnUpdate()
        {
            if (IsPause)
            {
                return;
            }

            Update();
        }
        
        private void OnFixedUpdate()
        {
            if (IsPause)
            {
                return;
            }

            FixedUpdate();
        }
        
        private void OnLateUpdate()
        {
            if (IsPause)
            {
                return;
            }

            LateUpdate();
        }
        
        protected void Pause()
        {
            IsPause = true;
        }

        protected void UnPause()
        {
            IsPause = false;
        }
        
        protected virtual void Dispose()
        {
            _monoUpdater.Unsubscribe(UpdateType.Update, OnUpdate);
            _monoUpdater.Unsubscribe(UpdateType.FixedUpdate, OnFixedUpdate);
            _monoUpdater.Unsubscribe(UpdateType.LateUpdate, OnLateUpdate);
        }
        
        #region IUpdateService

        bool IUpdatableService.IsPaused => IsPause;
        
        void IUpdatableService.Pause()
        {
            Pause();
        }

        void IUpdatableService.UnPause()
        {
            UnPause();
        }

        #endregion

        #region IDisposable

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}