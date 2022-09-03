namespace App.Localizations
{
    public interface ILocalizable
    {
        void TranslateViewModel(LocalizationData localizationData, Language language);
    }
}