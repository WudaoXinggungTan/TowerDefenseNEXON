using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Currency.Scripts
{
    public class CurrencyFactory : Factory
    {
        #region Variables

        [SerializeField] private CurrencyProduct currencyPrefab;

        #endregion

        #region Public Methods

        public override IProduct GetProduct(Vector3 position, Quaternion quaternion)
        {
            // Use ObjectPool
            GameObject instance = ObjectPoolManager.Instance.SpawnObject(currencyPrefab.gameObject, position, quaternion);
            CurrencyProduct newProduct = instance.GetComponent<CurrencyProduct>();
            //  each product contains its own logic
            newProduct.Initialize();
            return newProduct;
        }

        #endregion
    }
}