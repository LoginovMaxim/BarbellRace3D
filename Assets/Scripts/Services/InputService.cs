using App.Monos;
using UnityEngine;

namespace App.Services
{
    public class InputService : UpdatableService, IInputService
    {
        private readonly Joystick _joystick;
        
        private bool _isInputActive;
        private float _horizontal;
    
        public InputService(
            Joystick joystick,
            IMonoUpdater monoUpdater, 
            UpdateType updateType, 
            bool isImmediateStart) : 
            base(monoUpdater, updateType, isImmediateStart)
        {
            _joystick = joystick;
        }

        protected override void Update()
        {
            _isInputActive = Input.GetMouseButton(0);
            _horizontal = _joystick.Horizontal;
        }

        #region IInputService

        bool IInputService.IsInputActive => _isInputActive;
        float IInputService.Horizontal => _horizontal;

        #endregion
    }
}