using System.Threading.Tasks;
using Providers;
using UnityEngine;

namespace Assemblers.Parts
{
    public sealed class MapBuilder : IAssemblerPart
    {
        #region Constants

        private const int RoadOffset = 32;

        #endregion
        
        private readonly IMapSchemeConfigProvider _mapSchemeConfigProvider;
        private readonly IRoadPrefabsConfigProvider _roadPrefabsConfigProvider;
        
        public MapBuilder(IMapSchemeConfigProvider mapSchemeConfigProvider, IRoadPrefabsConfigProvider roadPrefabsConfigProvider)
        {
            _mapSchemeConfigProvider = mapSchemeConfigProvider;
            _roadPrefabsConfigProvider = roadPrefabsConfigProvider;
        }

        public Task Launch()
        {
            var currentRoadOffset = 0;
            foreach (var roadData in _mapSchemeConfigProvider.LevelData[0].RoadData)
            {
                var roadPrefab = _roadPrefabsConfigProvider.GetRoadPrefab(roadData.RoadType);
                var roadPosition = new Vector3(0, roadData.Height, currentRoadOffset);
                GameObject.Instantiate(roadPrefab, roadPosition, Quaternion.identity);
                
                currentRoadOffset += RoadOffset;
            }
            
            return Task.CompletedTask;
        }
    }
}