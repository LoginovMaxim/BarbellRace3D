using System;
using System.Collections.Generic;
using Monos;
using Providers;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "RoadPrefabsConfig", menuName = "BarbellRace3D/RoadPrefabsConfig")]
    public sealed class RoadPrefabsConfig : ScriptableObject, IRoadPrefabsConfigProvider
    {
        [SerializeField] private List<RoadPrefab> _roadPrefabs;

        private Road GetRoadPrefab(RoadType roadType)
        {
            foreach (var roadPrefab in _roadPrefabs)
            {
                if (roadPrefab.RoadType != roadType)
                {
                    continue;
                }

                return roadPrefab.Road;
            }

            throw new Exception($"Missing road prefab with {roadType} type");
        }
        
        #region IRoadPrefabsConfigProvider
        
        Road IRoadPrefabsConfigProvider.GetRoadPrefab(RoadType roadType)
        {
            return GetRoadPrefab(roadType);
        }

        #endregion
    }
}