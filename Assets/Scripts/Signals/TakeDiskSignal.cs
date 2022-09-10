using Views;

namespace Signals
{
    public sealed class TakeDiskSignal
    {
        public Disk Disk { get; }

        public TakeDiskSignal(Disk disk)
        {
            Disk = disk;
        }
    }
}