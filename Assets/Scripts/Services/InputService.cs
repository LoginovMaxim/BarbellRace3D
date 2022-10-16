using System;
using Monos;
using UnityEngine;

namespace Services
{
    public class InputService : UpdatableService, IInputService
    {
        private readonly InputReceiver _inputReceiver;
        
        private bool _isInputActive;
        private float _horizontal;

        public Action InputStarted { get; set; }
        public Action InputEnded { get; set; }

        public InputService(
            InputReceiver inputReceiver,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(monoUpdater, updateType, isImmediateStart)
        {
            _inputReceiver = inputReceiver;
        }

        protected override void Update()
        {
            _isInputActive = Input.GetMouseButton(0);
            _horizontal = _inputReceiver.Horizontal;

            if (Input.GetMouseButtonDown(0))
            {
                InputStarted.Invoke();
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                InputEnded.Invoke();
            }
        }

        #region IInputService

        bool IInputService.IsInputActive => _isInputActive;
        float IInputService.Horizontal => _horizontal;

        #endregion
    }
}