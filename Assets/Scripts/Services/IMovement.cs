using Views;

namespace App.Services
{
    public interface IMovement
    {
        MovementType MovementType { get; }
        void Enable();
        void Disable();
    }
}