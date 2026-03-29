using System;
using Features.Core.Scripts;
using UnityEngine;
using Features.Enemy.Scripts;

namespace Features.Currency.Scripts
{
    public class CurrencySpawnManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private CurrencyFactory currencyFactory;

        #endregion

        #region Private Methods

        private void Start()
        {
            EnemyProduct.OnEnemyDies += HandleEnemyDeath;
        }

        private void HandleEnemyDeath(int amount, Vector3 enemyPosition)
        {
            for (int i = 0; i <= amount; i++)
            {
                currencyFactory.GetProduct(enemyPosition);
            }
        }

        #endregion
    }
}