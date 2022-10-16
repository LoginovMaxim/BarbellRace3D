namespace Services
{
    public interface IBarbellMovementSystem
    {
        BarbellPositionType BarbellPositionType { get; set; }
        void Enable();
        void Disable();
    }
}