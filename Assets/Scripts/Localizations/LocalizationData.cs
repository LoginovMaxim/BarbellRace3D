using System;
using System.Collections.Generic;

namespace Localizations
{
    [Serializable]
    public class LocalizationData
    {
        public Dictionary<string, Dictionary<string, object>> Data;
    }
}