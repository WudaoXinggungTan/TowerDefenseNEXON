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
        
        #endregion

        #region Private Methods

        private void Start()
        {
            UpdateEnemyCountText();

            EnemySpawner.Instance.OnRemainingEnemyCountChanged += EnemySpawner_OnRemainingEnemyCountChanged;
        }

        private void EnemySpawner_OnRemainingEnemyCountChanged(object sender, EventArgs e)
        {
            UpdateEnemyCountText();
        }

        private void UpdateEnemyCountText()
        {
            enemyCountText.text = Convert.ToString(EnemySpawner.Instance.GetRemainingEnemy());
        }

        private void OnDisable()
        {
        }

        #endregion
    }
}