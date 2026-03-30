using System;
using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Tower.Scripts
{
    public class TowerHealth : MonoBehaviour, IDamageable, IHasProgress
    {
        #region Variables

        [SerializeField] private float towerMaxHealth = 5;
        private float towerHealth;

        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;

        #endregion

        #region Private Methods

        private void Start()
        {
            towerHealth = towerMaxHealth;
        }

        #endregion

        #region Interface Methods

        public void Damage(float amount)
        {
            towerHealth -= amount;
            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs { ProgressAmount = (towerHealth / towerMaxHealth) });

            if (towerHealth <= 0f)
            {
                ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            }
        }

        #endregion
    }
}