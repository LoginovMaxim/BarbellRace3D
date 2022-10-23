using Views;

namespace Signals
{
    public sealed class SetRailViewSignal
    {
        public RailView RailView { get; }

        public SetRailViewSignal(RailView railView)
        {
            RailView = railView;
        }
    }
}