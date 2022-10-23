using System.Collections.Generic;
using Configs;

namespace Providers
{
    public interface IMapSchemeConfigProvider
    {
        List<LevelData> LevelData { get; }
    }
}