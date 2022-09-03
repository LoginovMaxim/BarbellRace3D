using System.Collections.Generic;
using System.Linq;
using App.Localizations;
using UnityWeld.Binding;
using Zenject;

namespace App.UI.Screens.ViewModels
{
    [Binding] public abstract class ScreenViewModel : LocalizableViewModel
    {
        [Inject] private readonly SignalBus _signalBus;
        
        private List<ButtonChangeScreenViewModel> _buttonViewModels;
        
        private void Awake()
        {
            _buttonViewModels = GetComponentsInChildren<ButtonChangeScreenViewModel>().ToList();
            _buttonViewModels.ForEach(button => button.InjectSignalBus(_signalBus));
        }
    }
}