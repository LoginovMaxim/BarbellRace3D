namespace Services
{
    public interface IMovement
    {
        MovementType MovementType { get; }
        bool IsEnabled { get; }
        void Enable();
        void Disable();
    }
}