using Views;

namespace Services
{
    public interface IRailMovementSystem : IMovement
    {
        void SetRailView(RailView railView);
    }
}