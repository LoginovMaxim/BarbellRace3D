using System;
using UI;

namespace Localizations
{
    public abstract class LocalizableViewModel : ViewModel, ILocalizable
    {
        public virtual void TranslateViewModel(LocalizationData localizationData, Language language)
        {
            foreach (var property in GetType().GetProperties())
            {
                if (property.PropertyType != typeof(string))
                {
                    continue;
                }
                
                var key = localizationData.Data.ContainsKey(property.Name) ? property.Name : (string) property.GetValue(this);
                var languageName = Enum.GetName(typeof(Language), language);
                
                if (!localizationData.Data[key].TryGetValue(languageName, out var translateProperty))
                {
                    continue;
                }

                property.SetValue(this, translateProperty);
            }
        }
    }
}