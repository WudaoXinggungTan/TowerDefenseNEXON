using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Tower.Scripts
{
    public class TowerFactory : Factory
    {
        #region Variables

        [SerializeField] private TowerProduct towerPrefab;

        #endregion

        #region Public Methods

        public override IProduct GetProduct(Vector3 position, Quaternion quaternion)
        {
            // Use ObjectPool
            GameObject instance = ObjectPoolManager.Instance.SpawnObject(towerPrefab.gameObject, position, quaternion);
            TowerProduct newProduct = instance.GetComponent<TowerProduct>();
            //  each product contains its own logic
            newProduct.Initialize();
            return newProduct;
        }

        #endregion
    }
}