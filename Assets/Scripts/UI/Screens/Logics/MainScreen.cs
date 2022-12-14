using UI.Screens.ViewModels;

namespace UI.Screens.Logics
{
    public class MainScreen : BaseScreen
    {
        protected override ScreenId ScreenId => ScreenId.Main;
        
        private MainScreenViewModel _viewModel;

        public MainScreen(MainScreenViewModel.Factory metaMainScreenViewModelFactory)
        {
            _viewModel = metaMainScreenViewModelFactory.Create();
            SetViewModel(_viewModel);
        }
    }
}