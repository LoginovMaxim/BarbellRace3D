namespace Assemblers
{
    public interface IAssembler
    {
        int ServicesCount { get; }
        int CurrentStepCount { get; }
        float Progress { get; }
    }
}