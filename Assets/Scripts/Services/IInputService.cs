namespace App.Services
{
    public interface IInputService
    {
        public bool IsInputActive { get; }
        float Horizontal { get; }
    }
}