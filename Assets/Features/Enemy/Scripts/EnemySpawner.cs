using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Features.Core.Scripts;

namespace Features.Enemy.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        #region Variables

        [Tooltip("The enemy will spawn after 'x' amount of time delay")]
        [SerializeField] private float timeDelay = 1f;
        [Tooltip("How fast the system will keep trying to spawn enemy")]
        [SerializeField] private float repeatRate = .1f;

        [SerializeField] private FactoriesDataScriptableObject enemyFactoriesData;

        private Dictionary<Factory, bool> factoriesCountdownDictionary;
        public static EnemySpawner Instance { get; private set; }
        public event EventHandler OnSpawnCountChanged;

        #endregion

        #region Private Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            factoriesCountdownDictionary = new Dictionary<Factory, bool>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnObject), timeDelay, repeatRate);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (enemyFactoriesData != null)
            {
                enemyFactoriesData.ResetSpawnCounts();

                OnSpawnCountChanged?.Invoke(this, EventArgs.Empty);
            }

            MapKeyValueDictionary();
        }

        private void MapKeyValueDictionary()
        {
            if (factoriesCountdownDictionary == null)
            {
                factoriesCountdownDictionary = new Dictionary<Factory, bool>();
            }
            else
            {
                factoriesCountdownDictionary.Clear();
            }

            foreach (FactoriesDataScriptableObject.FactorySpawnData factorySpawnData in enemyFactoriesData.factoriesList)
            {
                if (factorySpawnData.factoryType != null)
                {
                    factoriesCountdownDictionary[factorySpawnData.factoryType] = factorySpawnData.OnCooldown;
                }
            }
        }

        // Pick a random Factory in the Factory list
        private FactoriesDataScriptableObject.FactorySpawnData PickRandomFactoryData()
        {
            // Reference the list to short the name down (too long)
            List<FactoriesDataScriptableObject.FactorySpawnData> factoriesList = enemyFactoriesData.factoriesList;

            int diceRoll = (Random.Range(0, 100));
            int cumulative = 0;

            for (int i = 0; i < factoriesList.Count; i++)
            {
                cumulative += factoriesList[i].probability;
                if (diceRoll < cumulative)
                {
                    return factoriesList[i];
                }
            }

            return null;
        }

        private GameObject PickRandomSpawnPosition(FactoriesDataScriptableObject.FactorySpawnData factoryData)
        {
            if (factoryData.spawnPositionList == null || factoryData.spawnPositionList.Count == 0)
            {
                return null;
            }

            return factoryData.spawnPositionList[Random.Range(0, factoryData.spawnPositionList.Count)];
        }

        private void SpawnObject()
        {
            if (!GameManager.Instance.IsGamePlaying())
            {
                return;
            }

            FactoriesDataScriptableObject.FactorySpawnData factoryData = PickRandomFactoryData();
            if (factoryData == null)
            {
                return;
            }

            //No other way to find the position GameObject in the runtime (Scriptable Object not allows a reference Hierarchy game object)
            Transform factorySpawnPoint = GameObject.Find(PickRandomSpawnPosition(factoryData).name).transform;

            //float randomX = factorySpawnPoint.position.x + Random.Range(-factorySpawnPoint.localScale.x, factorySpawnPoint.localScale.x);

            Vector3 spawnPos = new Vector3(factorySpawnPoint.position.x, factorySpawnPoint.position.y, factorySpawnPoint.position.z);

            // If that factory is on cooldown -> return
            if (factoriesCountdownDictionary[factoryData.factoryType] == true)
            {
                return;
            }

            // If that factory is out of product count
            if (factoryData.spawnCount <= 0)
            {
                return;
            }

            factoryData.spawnCount--;
            OnSpawnCountChanged?.Invoke(this, EventArgs.Empty);
            factoryData.factoryType.GetProduct(spawnPos, factorySpawnPoint.rotation);

            // Each Factory will have its own cooldown, store in a dictionary. When GetProduct() got called, start the cooldown on that factory.
            StartCoroutine(GetProductRoutine(factoryData));
        }

        private IEnumerator GetProductRoutine(FactoriesDataScriptableObject.FactorySpawnData factoryData)
        {
            factoriesCountdownDictionary[factoryData.factoryType] = true;

            yield return new WaitForSeconds(factoryData.cooldown);

            factoriesCountdownDictionary[factoryData.factoryType] = false;
        }

        #endregion
    }
}