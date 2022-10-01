using UI.Popups;

namespace UI.Services
{
    public interface IPopupService
    {
        bool IsSomePopupShowing { get; }
        
        void ShowPopup(IconPopupData iconPopupData);
    }
}