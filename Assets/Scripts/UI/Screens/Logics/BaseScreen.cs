using App.UI.Screens.ViewModels;

namespace App.UI.Screens.Logics
{
    public abstract class BaseScreen : IScreen
    {
        private ScreenViewModel _screenViewModel;

        protected abstract ScreenId ScreenId { get; }
        
        protected void SetViewModel(ScreenViewModel screenViewModel)
        {
            _screenViewModel = screenViewModel;
        }
        
        protected virtual void Update()
        {
        }
        
        private void SetActive(bool isActive)
        {
            _screenViewModel.SetActive(isActive);
        }
        
        #region IScreen

        ScreenId IScreen.ScreenId => ScreenId;

        void IScreen.SetActive(bool isActive)
        {
            SetActive(isActive);
        }

        void IScreen.Update()
        {
            Update();
        }

        #endregion
    }
}