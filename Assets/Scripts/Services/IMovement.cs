using Views;

namespace App.Services
{
    public interface IMovement
    {
        MovementType MovementType { get; }
        void Pause();
        void UnPause();
    }
}