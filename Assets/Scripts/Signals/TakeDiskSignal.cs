namespace Signals
{
    public sealed class TakeDiskSignal
    {
        public int DiskCount { get; private set; }

        public TakeDiskSignal(int diskCount)
        {
            DiskCount = diskCount;
        }
    }
}