using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;


namespace Features.Enemy.Scripts
{
    public class EnemyFactory : Factory
    {
        #region Variables

        [SerializeField] private EnemyProduct enemyPrefab;

        #endregion

        #region Public Methods

        public override IProduct GetProduct(Vector3 position)
        {
            // Use ObjectPool
            GameObject instance = ObjectPoolManager.Instance.SpawnObject(enemyPrefab.gameObject, position, Quaternion.identity);
            EnemyProduct newProduct = instance.GetComponent<EnemyProduct>();
            //  each product contains its own logic
            newProduct.Initialize();
            return newProduct;
        }

        #endregion
    }
}