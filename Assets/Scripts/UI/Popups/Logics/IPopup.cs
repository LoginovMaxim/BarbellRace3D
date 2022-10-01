namespace UI.Popups.Logics
{
    public interface IPopup
    {
        bool Spawned { get; }
        void Despawn(int index);
    }
}