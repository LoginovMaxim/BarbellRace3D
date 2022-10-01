using System;

namespace Services
{
    public interface IInputService
    {
        public bool IsInputActive { get; }
        float Horizontal { get; }
        Action InputStarted { get; set; }
        Action InputEnded { get; set; }
    }
}