using System;
using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Enemy.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyProduct : MonoBehaviour, IProduct, IDamageable, IHasProgress
    {
        #region Variables

        public string ProductName { get; }
        public bool IsInitialized { get; private set; }

        [SerializeField] private float enemyMaxHealth = 3;
        private float enemyHealth;
        [SerializeField] private int currencyDropAmount = 10;
        public static event Action<int, Vector3> OnEnemyDies;
        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;

        #endregion

        #region Interface Methods

        public void Initialize()
        {
            IsInitialized = true;
            enemyHealth = enemyMaxHealth;
        }

        public void Damage(float amount)
        {
            enemyHealth -= amount;
            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs { ProgressAmount = (enemyHealth / enemyMaxHealth) });

            if (enemyHealth <= 0f)
            {
                OnEnemyDies?.Invoke(currencyDropAmount, transform.position);
                ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            }
        }

        #endregion
    }
}