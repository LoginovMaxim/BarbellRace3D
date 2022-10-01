using System;
using Services;
using UnityEngine;

namespace Monos
{
    public class MonoUpdater : MonoBehaviour, IMonoUpdater
    {
        public event Action Updated;
        public event Action FixedUpdated;
        public event Action LateUpdated;

        private void Awake() => DontDestroyOnLoad(gameObject);

        private void Update() { Updated?.Invoke(); }
        private void FixedUpdate() { FixedUpdated?.Invoke(); }
        private void LateUpdate() { LateUpdated?.Invoke(); }

        private void Subscribe(UpdateType updateType, Action action)
        {
            switch (updateType)
            {
                case UpdateType.Update:
                {
                    Updated += action;
                    break;
                }
                case UpdateType.FixedUpdate:
                {
                    FixedUpdated += action;
                    break;
                }
                case UpdateType.LateUpdate:
                {
                    LateUpdated += action;
                    break;
                }
            }
        }
        
        private void Unsubscribe(UpdateType updateType, Action action)
        {
            switch (updateType)
            {
                case UpdateType.Update:
                {
                    Updated -= action;
                    break;
                }
                case UpdateType.FixedUpdate:
                {
                    FixedUpdated -= action;
                    break;
                }
                case UpdateType.LateUpdate:
                {
                    LateUpdated -= action;
                    break;
                }
            }
        }

        #region IMonoUpdater

        void IMonoUpdater.Subscribe(UpdateType updateType, Action action)
        {
            Subscribe(updateType, action);
        }
        
        void IMonoUpdater.Unsubscribe(UpdateType updateType, Action action)
        {
            Unsubscribe(updateType, action);
        }

        #endregion
    }
}
