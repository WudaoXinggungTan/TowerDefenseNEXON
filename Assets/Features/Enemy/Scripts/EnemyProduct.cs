using System;
using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Enemy.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyProduct : MonoBehaviour, IProduct, IDamageable
    {
        #region Variables

        public string ProductName { get; }
        public bool IsInitialized { get; private set; }

        [SerializeField] private float enemyHealth;
        [SerializeField] private int currencyDropAmount = 10;
        public static event Action<int, Vector3> OnEnemyDies;

        #endregion

        #region Interface Methods

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void Damage(float amount)
        {
            enemyHealth -= amount;

            if (enemyHealth <= 0f)
            {
                OnEnemyDies?.Invoke(currencyDropAmount, transform.position);
                ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            }
        }

        #endregion
    }
}