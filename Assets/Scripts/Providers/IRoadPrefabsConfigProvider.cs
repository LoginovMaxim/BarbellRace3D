using Configs;
using Monos;

namespace Providers
{
    public interface IRoadPrefabsConfigProvider
    {
        Road GetRoadPrefab(RoadType roadType);
    }
}