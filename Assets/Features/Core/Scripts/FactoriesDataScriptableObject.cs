using System;
using UnityEngine;
using System.Collections.Generic;
using Features.Core.Scripts;

namespace Features.Core.Scripts
{
    [CreateAssetMenu(fileName = "FactoriesSpawnProbabilityScriptableObject", menuName = "Scriptable Objects/FactoriesSpawnProbabilityScriptableObject")]
    public class FactoriesDataScriptableObject : ScriptableObject
    {
        [Serializable]
        public class FactorySpawnData
        {
            #region Variables

            public Factory factoryType;
            [Range(0, 100)] public int probability = 0;
            public float cooldown = 1f;

            public List<GameObject> spawnPositionList;
            public int spawnCount = 100;

            #endregion

            #region Private Variables

            private bool onCooldown;

            #endregion

            #region Property

            public bool OnCooldown => onCooldown;

            #endregion
        }

        public List<FactorySpawnData> factoriesList;

        public int GetTotalSpawnCount()
        {
            int totalSpawnCount = 0;
            foreach (var factorySpawnData in factoriesList)
            {
                totalSpawnCount += factorySpawnData.spawnCount;
            }

            return totalSpawnCount;
        }
    }
}