using App.UI.Popups;

namespace App.UI.Services
{
    public interface IPopupService
    {
        bool IsSomePopupShowing { get; }
        
        void ShowPopup(IconPopupData iconPopupData);
    }
}