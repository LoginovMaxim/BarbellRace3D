using System.Threading.Tasks;

namespace Localizations
{
    public interface ILocalizationDataProvider
    {
        LocalizationData LocalizationData { get; }
        
        Task Load();
    }
}