using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Projectile.Scripts
{
    public class ProjectileFactory : Factory
    {
        #region Variables

        [SerializeField] private ProjectileProduct projectileProduct;

        #endregion

        #region Public Methods

        public override IProduct GetProduct(Vector3 position)
        {
            // Use ObjectPool
            GameObject instance = ObjectPoolManager.Instance.SpawnObject(projectileProduct.gameObject, position, Quaternion.identity);
            ProjectileProduct newProduct = instance.GetComponent<ProjectileProduct>();
            //  each product contains its own logic
            newProduct.Initialize();
            return newProduct;
        }

        #endregion
    }
}