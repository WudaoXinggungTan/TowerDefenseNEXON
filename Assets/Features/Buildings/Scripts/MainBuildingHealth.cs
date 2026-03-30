using System;
using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Buildings.Scripts
{
    public class MainBuildingHealth : MonoBehaviour, IDamageable, IHasProgress
    {
        #region Variables

        [SerializeField] private float mainBuildingMaxHealth = 10;
        private float mainBuildingHealth;
        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;

        #endregion

        #region Private Methods

        private void Start()
        {
            mainBuildingHealth = mainBuildingMaxHealth;
        }

        #endregion

        #region Interface Methods

        public void Damage(float amount)
        {
            mainBuildingHealth -= amount;
            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs { ProgressAmount = (mainBuildingHealth / mainBuildingMaxHealth) });
            SoundManager.Instance.PlaySound(AudioClipRefsScriptableObject.Instance.projectileHit, transform.position);

            if (mainBuildingHealth <= 0f)
            {
                GameManager.Instance.EndTheGame();
            }
        }

        #endregion
    }
}