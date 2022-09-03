namespace App.Services
{
    public interface IUpdatableService : IService
    {
        bool IsPaused { get; }
        
        void Pause();
        void UnPause();
    }
}