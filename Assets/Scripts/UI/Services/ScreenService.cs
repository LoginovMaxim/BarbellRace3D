using System.Collections.Generic;
using System.Linq;
using App.UI.Screens;
using App.UI.Screens.Logics;
using App.UI.Signals;
using Zenject;

namespace App.UI.Services
{
    public sealed class ScreenService : IScreenService
    {
        private readonly List<IScreen> _screens;
        private readonly SignalBus _signalBus;
        
        private IScreen _previousScreen;
        private IScreen _currentScreen;

        public ScreenService(List<IScreen> screens, SignalBus signalBus)
        {
            _screens = screens;
            _screens.ForEach(screen =>
            {
                screen.SetActive(false);
            });

            _signalBus = signalBus;
            _signalBus.Subscribe<SwitchScreenSignal>(x => OnChangeScreenButtonClicked(x.ScreenId));
            
            _currentScreen = _screens[0];
            _currentScreen.SetActive(true);
        }

        private void OnChangeScreenButtonClicked(ScreenId screenId)
        {
            if (screenId == ScreenId.Back)
            {
                OnBackScreenButtonClicked();
                return;
            }
            
            var screen = _screens.FirstOrDefault(screen => screen.ScreenId == screenId);
            if (screen == null)
            {
                return;
            }

            _previousScreen = _currentScreen;
            _currentScreen.SetActive(false);
            _currentScreen = screen;
            _currentScreen.Update();
            _currentScreen.SetActive(true);
        }

        private void OnBackScreenButtonClicked()
        {
            if (_previousScreen == null || _previousScreen == _currentScreen)
            {
                return;
            }

            var currentScreen = _currentScreen;
            _currentScreen.SetActive(false);
            _currentScreen = _previousScreen;
            _currentScreen.Update();
            _currentScreen.SetActive(true);
            _previousScreen = currentScreen;
        }

        #region IMetaScreenService

        public IScreen CurrentScreen => _currentScreen;
        
        void IScreenService.OnChangeScreenButtonClicked(ScreenId screenId)
        {
            OnChangeScreenButtonClicked(screenId);
        }

        #endregion
    }
}