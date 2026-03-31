using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Features.Core.Scripts;
using Features.Enemy.Scripts;


namespace Features.UI.Scripts
{
    public class EnemyCountUI : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image enemyCountImage;
        [SerializeField] private TextMeshProUGUI enemyCountText;

        [SerializeField] private FactoriesDataScriptableObject factoriesData;

        #endregion

        #region Private Methods

        private void Start()
        {
            InitializeEnemyCountText();
            EnemyProduct.OnEnemyDies += DecrementEnemyCount;
        }

        private void DecrementEnemyCount(int currency, Vector3 position)
        {
            UpdateEnemyCountText();

            if (factoriesData.GetCurrentEnemyCount() <= 0)
            {
                GameManager.Instance.EndTheGame();
            }
        }

        private void InitializeEnemyCountText()
        {
            enemyCountText.text = Convert.ToString(factoriesData.GetTotalSpawnCount());
        }

        private void UpdateEnemyCountText()
        {
            enemyCountText.text = Convert.ToString(factoriesData.GetCurrentEnemyCount());
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            EnemyProduct.OnEnemyDies -= DecrementEnemyCount;
        }

        #endregion
    }
}