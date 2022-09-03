using UnityWeld.Binding;
using Zenject;

namespace App.UI.Screens.ViewModels
{
    [Binding] public sealed class MainScreenViewModel : ScreenViewModel
    {
        public class Factory : PlaceholderFactory<MainScreenViewModel>
        {
        }
    }
}