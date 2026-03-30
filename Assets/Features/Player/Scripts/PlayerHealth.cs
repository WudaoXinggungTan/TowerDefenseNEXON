using System;
using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Player.Scripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable, IHasProgress
    {
        #region Variables

        [SerializeField] private float playerMaxHealth = 5;
        private float playerHealth;

        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;

        #endregion

        #region Private Methods

        private void Start()
        {
            playerHealth = playerMaxHealth;
        }

        #endregion

        #region Interface Methods

        public void Damage(float amount)
        {
            playerHealth -= amount;
            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs { ProgressAmount = (playerHealth / playerMaxHealth) });
            SoundManager.Instance.PlaySound(AudioClipRefsScriptableObject.Instance.projectileHit, transform.position);

            if (playerHealth <= 0f)
            {
                GameManager.Instance.EndTheGame();
            }
        }

        #endregion
    }
}