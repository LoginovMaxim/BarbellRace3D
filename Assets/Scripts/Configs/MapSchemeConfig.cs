using System.Collections.Generic;
using Providers;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MapSchemeConfig", menuName = "BarbellRace3D/MapSchemeConfig")]
    public sealed class MapSchemeConfig : ScriptableObject, IMapSchemeConfigProvider
    {
        [SerializeField] private List<LevelData> _levelData;

        #region IMapSchemeConfigProvider

        List<LevelData> IMapSchemeConfigProvider.LevelData => _levelData;

        #endregion
    }
}