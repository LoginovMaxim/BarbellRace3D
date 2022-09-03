using System.Threading.Tasks;

namespace App.Localizations
{
    public interface ILocalizationDataProvider
    {
        LocalizationData LocalizationData { get; }
        
        Task Load();
    }
}