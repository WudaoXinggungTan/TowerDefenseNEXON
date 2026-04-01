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

        [SerializeField] private int currencyDropAmount = 10;
        [SerializeField] private float enemyMaxHealth = 3;
        private float enemyHealth;
        private bool isReleased = false;

        public static event Action<int, Vector3> OnEnemyDies;
        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;

        #endregion

        #region Private Methods

        private void OnEnable()
        {
            isReleased = false;
            enemyHealth = enemyMaxHealth;
        }

        #endregion

        #region Interface Methods

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void Damage(float amount)
        {
            enemyHealth -= amount;
            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs { ProgressAmount = (enemyHealth / enemyMaxHealth) });
            SoundManager.Instance.PlaySound(AudioClipRefsScriptableObject.Instance.projectileHit, transform.position);

            if (enemyHealth <= 0f)
            {
                HandleEnemyDeath();
            }
        }

        private void HandleEnemyDeath()
        {
            if (isReleased)
            {
                return;
            }

            isReleased = true;
            
            SoundManager.Instance.PlaySound(AudioClipRefsScriptableObject.Instance.enemyDeath, transform.position);
            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            OnEnemyDies?.Invoke(currencyDropAmount, transform.position);
        }

        #endregion
    }
}