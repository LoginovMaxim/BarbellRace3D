﻿using App.Monos;

namespace App.Services
{
    public class InputService : UpdatableService, IInputService
    {
        private readonly Joystick _joystick;
        
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
            _horizontal = _joystick.Horizontal;
        }

        #region IInputService

        float IInputService.Horizontal => _horizontal;

        #endregion
    }
}