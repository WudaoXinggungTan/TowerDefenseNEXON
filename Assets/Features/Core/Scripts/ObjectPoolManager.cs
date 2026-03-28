using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Features.Core.Scripts
{
    public class ObjectPoolManager : MonoBehaviour
    {
        #region Variables

        public static ObjectPoolManager Instance { get; private set; }

        [FormerlySerializedAs("AddToDontDestroyOnLoad")] [SerializeField]
        private bool addToDontDestroyOnLoad = false;

        private GameObject emptyHolder;
        private GameObject gameObjectsEmpty;
        private GameObject particleSystemsEmpty;
        private GameObject soundFXEmpty;

        private Dictionary<GameObject, ObjectPool<GameObject>> objectPools =
            new Dictionary<GameObject, ObjectPool<GameObject>>();

        private Dictionary<GameObject, GameObject> cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

        public enum PoolType
        {
            GameObjects,
            ParticleSystems,
            SoundFX,
        }

        private PoolType poolingType;

        #endregion

        #region Private Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            SetupEmpties();
        }

        private void SetupEmpties()
        {
            emptyHolder = new GameObject("Object Pools");

            gameObjectsEmpty = new GameObject("Game Objects");
            gameObjectsEmpty.transform.SetParent(emptyHolder.transform);

            particleSystemsEmpty = new GameObject("Particle Systems");
            particleSystemsEmpty.transform.SetParent(emptyHolder.transform);

            soundFXEmpty = new GameObject("Sound Effects");
            soundFXEmpty.transform.SetParent(emptyHolder.transform);

            if (addToDontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObjectsEmpty.transform.root);
            }
        }


        private void CreatePool(GameObject prefab, Vector3 position, Quaternion rotation,
                                PoolType   poolType = PoolType.GameObjects)
        {
            ObjectPool<GameObject> pool =
                new ObjectPool<GameObject>(createFunc: () => CreateGameObject(prefab, position, rotation, poolType),
                                           actionOnGet: OnGetGameObject, actionOnDestroy: OnDestroyGameObject,
                                           actionOnRelease: OnReleaseGameObject, collectionCheck: true,
                                           defaultCapacity: 10, maxSize: 100);
            objectPools.Add(prefab, pool);


            /*
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                                                                     createFunc: () =>
                                                                                 {
                                                                                     prefab.SetActive(false);

                                                                                     GameObject gameObj =
                                                                                         Instantiate(prefab, position,
                                                                                                     rotation);

                                                                                     prefab.SetActive(true);

                                                                                     //GameObject parentObject = SetParentObject(poolType);
                                                                                     //gameObj.transform.SetParent(parentObject.transform);
                                                                                     return prefab;
                                                                                 },
                                                                     actionOnGet: (GameObject gameObj) =>
                                                                                  {
                                                                                      // Optional when get object
                                                                                  },
                                                                     actionOnRelease: (GameObject gameObj) =>
                                                                                      {
                                                                                          gameObj.SetActive(false);
                                                                                      },
                                                                     actionOnDestroy: (GameObject gameObj) =>
                                                                                      {
                                                                                          if (cloneToPrefabMap
                                                                                             .ContainsKey(gameObj))
                                                                                          {
                                                                                              cloneToPrefabMap
                                                                                                 .Remove(gameObj);
                                                                                          }
                                                                                      }
                                                                    );
            */
        }

        private void CreatePool(GameObject prefab, Transform parent, Quaternion rotation,
                                PoolType   poolType = PoolType.GameObjects)
        {
            // Need to learn more about Delegate to understand this Constructor!
            ObjectPool<GameObject> pool =
                new ObjectPool<GameObject>(createFunc: () => CreateGameObject(prefab, parent, rotation, poolType),
                                           actionOnGet: OnGetGameObject, actionOnDestroy: OnDestroyGameObject,
                                           actionOnRelease: OnReleaseGameObject, collectionCheck: true,
                                           defaultCapacity: 10, maxSize: 100);

            objectPools.Add(prefab, pool);
        }

        private GameObject CreateGameObject(GameObject prefab, Vector3 position, Quaternion rotation,
                                            PoolType   poolType = PoolType.GameObjects)
        {
            prefab.SetActive(false);

            GameObject gameObj = Instantiate(prefab, position, rotation);

            prefab.SetActive(true);

            GameObject parentObject = SetParentObject(poolType);
            gameObj.transform.SetParent(parentObject.transform);
            return gameObj;
        }

        private GameObject CreateGameObject(GameObject prefab, Transform parent, Quaternion rotation,
                                            PoolType   poolType = PoolType.GameObjects)
        {
            prefab.SetActive(false);

            GameObject gameObj = Instantiate(prefab, parent);

            prefab.SetActive(true);

            return gameObj;
        }

        private void OnGetGameObject(GameObject gameObj)
        {
            //Debug.Log("Optional when get");
        }

        private void OnReleaseGameObject(GameObject gameObj)
        {
            gameObj.SetActive(false);
        }

        private void OnDestroyGameObject(GameObject gameObj)
        {
            if (cloneToPrefabMap.ContainsKey(gameObj))
            {
                cloneToPrefabMap.Remove(gameObj);
            }
        }

        private GameObject SetParentObject(PoolType poolType)
        {
            switch (poolType)
            {
                default:
                    return null;
                case PoolType.GameObjects:
                    return gameObjectsEmpty;
                case PoolType.ParticleSystems:
                    return particleSystemsEmpty;
                case PoolType.SoundFX:
                    return soundFXEmpty;
            }
        }

        // ---------------------------------------------- Vector3 ------------------------------------------------------
        private T SpawnObject<T>(GameObject originalPrefab, Vector3 position, Quaternion rotation,
                                 PoolType   poolType = PoolType.GameObjects) where T : Object
        {
            if (!objectPools.ContainsKey(originalPrefab))
            {
                CreatePool(originalPrefab, position, rotation, poolType);
            }

            GameObject cloneGameObject = objectPools[originalPrefab].Get();
            if (cloneGameObject != null)
            {
                if (!cloneToPrefabMap.ContainsKey(cloneGameObject))
                {
                    cloneToPrefabMap.Add(cloneGameObject, originalPrefab);
                }

                cloneGameObject.transform.position = position;
                cloneGameObject.transform.rotation = rotation;
                cloneGameObject.SetActive(true);

                if (typeof(T) == typeof(GameObject))
                {
                    return cloneGameObject as T;
                }

                T component = cloneGameObject.GetComponent<T>();

                if (component == null)
                {
                    Debug.LogError($"Object {originalPrefab.name} doesn't have component of type {typeof(T)}");
                    return null;
                }

                return component;
            }

            return null;
        }

        #endregion

        #region Pulic Methods

        public T SpawnObject<T>(T        typePrefab, Vector3 position, Quaternion rotation,
                                PoolType poolType = PoolType.GameObjects) where T : Component
        {
            return SpawnObject<T>(typePrefab.gameObject, position, rotation, poolType);
        }

        public GameObject SpawnObject(GameObject originalPrefab, Vector3 position, Quaternion rotation,
                                      PoolType   poolType = PoolType.GameObjects)
        {
            return SpawnObject<GameObject>(originalPrefab, position, rotation, poolType);
        }

        #endregion

        #region Private Methods

        // ---------------------------------------------- Parent ------------------------------------------------------

        private T SpawnObject<T>(GameObject originalPrefab, Transform parent, Quaternion rotation,
                                 PoolType   poolType = PoolType.GameObjects) where T : Object
        {
            if (!objectPools.ContainsKey(originalPrefab))
            {
                CreatePool(originalPrefab, parent, rotation, poolType);
            }

            GameObject gameObj = objectPools[originalPrefab].Get();

            if (gameObj != null)
            {
                if (!cloneToPrefabMap.ContainsKey(gameObj))
                {
                    cloneToPrefabMap.Add(gameObj, originalPrefab);
                }

                gameObj.transform.SetParent(parent);
                gameObj.transform.localPosition = Vector3.zero;
                gameObj.transform.localRotation = rotation;
                gameObj.transform.localScale    = Vector3.one;

                gameObj.SetActive(true);

                if (typeof(T) == typeof(GameObject))
                {
                    return gameObj as T;
                }

                T component = gameObj.GetComponent<T>();

                if (component == null)
                {
                    Debug.LogError($"Object {originalPrefab.name} doesn't have component of type {typeof(T)}");
                    return null;
                }

                return component;
            }

            return null;
        }

        #endregion

        #region Pulic Methods

        public T SpawnObject<T>(T        typePrefab, Transform parent, Quaternion rotation,
                                PoolType poolType = PoolType.GameObjects) where T : Component
        {
            return SpawnObject<T>(typePrefab.gameObject, parent, rotation, poolType);
        }

        public GameObject SpawnObject(GameObject originalPrefab, Transform parent, Quaternion rotation,
                                      PoolType   poolType = PoolType.GameObjects)
        {
            return SpawnObject<GameObject>(originalPrefab, parent, rotation, poolType);
        }

        public void ReturnObjectToPool(GameObject gameObj, PoolType poolType = PoolType.GameObjects)
        {
            if (cloneToPrefabMap.TryGetValue(gameObj, out GameObject prefab))
            {
                GameObject parentObject = SetParentObject(poolType);

                if (gameObj.transform.parent != parentObject.transform)
                {
                    gameObj.transform.SetParent(parentObject.transform);
                }

                if (objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
                {
                    pool.Release(gameObj);
                }
            }
            else
            {
                Debug.LogError($"Trying to return an object that is not pooled: {gameObj.name}");
            }
        }

        #endregion
    }
}
