using UI.Signals;
using UnityEngine;
using UnityWeld.Binding;
using Zenject;

namespace UI.Screens.ViewModels
{
    [Binding] public class ButtonChangeScreenViewModel : ViewModel
    {
        [SerializeField] private ScreenId _screenId;

        private SignalBus _signalBus;
        
        [Binding] public void OnChangeScreenButtonClicked()
        {
            _signalBus.Fire(new SwitchScreenSignal(_screenId));
        }

        public void InjectSignalBus(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
    }
}