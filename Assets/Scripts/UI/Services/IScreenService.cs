using UI.Screens;
using UI.Screens.Logics;

namespace UI.Services
{
    public interface IScreenService
    {
        IScreen CurrentScreen { get; }
        void OnChangeScreenButtonClicked(ScreenId screenId);
    }
}