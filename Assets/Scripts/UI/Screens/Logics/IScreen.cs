namespace UI.Screens.Logics
{
    public interface IScreen
    {
        ScreenId ScreenId { get; }
        
        void SetActive(bool isActive);
        void Update();
    }
}