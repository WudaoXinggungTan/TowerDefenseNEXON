using System;
using Features.Core.Scripts;
using UnityEngine;
using Features.Enemy.Scripts;

namespace Features.Currency.Scripts
{
    public class CurrencySpawnManager : MonoBehaviour
    {
        #region Variables

        public static CurrencySpawnManager Instance { get; private set; }

        [SerializeField] private CurrencyFactory currencyFactory;

        #endregion

        #region Private Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            EnemyProduct.OnEnemyDies += HandleEnemyDeath;
        }

        private void OnDisable()
        {
            EnemyProduct.OnEnemyDies -= HandleEnemyDeath;
        }

        private void HandleEnemyDeath(int amount, Vector3 enemyPosition)
        {
            for (int i = 0; i < amount; i++)
            {
                currencyFactory.GetProduct(enemyPosition, new Quaternion());
            }

            SoundManager.Instance.PlaySound(AudioClipRefsScriptableObject.Instance.currencyDrop, transform.position);
        }

        #endregion
    }
}