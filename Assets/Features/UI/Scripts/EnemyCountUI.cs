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

        private int totalEnemiesCount;

        #endregion

        #region Private Methods

        private void Start()
        {
            Show();
            totalEnemiesCount = factoriesData.GetTotalSpawnCount();
            UpdateEnemyCountText();
            EnemyProduct.OnEnemyDies += (i, vector3) =>
            {
                totalEnemiesCount--;
                UpdateEnemyCountText();

                if (totalEnemiesCount <= 0)
                {
                    GameManager.Instance.EndTheGame();
                }
            };
        }

        private void UpdateEnemyCountText()
        {
            enemyCountText.text = Convert.ToString(totalEnemiesCount);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}