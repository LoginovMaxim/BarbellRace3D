using App.UI.Screens;
using App.UI.Screens.Logics;

namespace App.UI.Services
{
    public interface IScreenService
    {
        IScreen CurrentScreen { get; }
        void OnChangeScreenButtonClicked(ScreenId screenId);
    }
}