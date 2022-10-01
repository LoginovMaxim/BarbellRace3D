using UnityWeld.Binding;
using Zenject;

namespace UI.Screens.ViewModels
{
    [Binding] public sealed class MainScreenViewModel : ScreenViewModel
    {
        public class Factory : PlaceholderFactory<MainScreenViewModel>
        {
        }
    }
}