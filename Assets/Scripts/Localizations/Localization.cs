using System.Threading.Tasks;
using Assemblers;
using Monos;

namespace Localizations
{
    public class Localization : IAssemblerPart
    {
        private readonly IGameObjectFinder _gameObjectFinder;
        private readonly ILocalizationDataProvider _localizationDataProvider;

        private Language _language = Language.EN;
        
        public Localization(
            IGameObjectFinder gameObjectFinder,
            ILocalizationDataProvider localizationDataProvider)
        {
            _gameObjectFinder = gameObjectFinder;
            _localizationDataProvider = localizationDataProvider;
        }

        public async Task Launch()
        {
            await _localizationDataProvider.Load();
            
            var localizableViewModels = _gameObjectFinder.GetGameObjects<LocalizableViewModel>();

            foreach (var localizableViewModel in localizableViewModels)
            {
                localizableViewModel.TranslateViewModel(_localizationDataProvider.LocalizationData, _language);
            }
        }
    }
}