using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Player.Scripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        #region Variables

        [SerializeField] private float playerHealth;

        #endregion

        #region Interface Methods

        public void Damage(float amount)
        {
            playerHealth -= amount;

            if (playerHealth <= 0f)
            {
                GameManager.Instance.EndTheGame();
            }
        }

        #endregion
    }
}