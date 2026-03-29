using System;
using UnityEngine;

namespace Features.Player.Scripts
{
    public class PlayerCurrency : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int playerCurrentCurrency;

        public int PlayerCurrentCurrency { get; private set; }
        public event EventHandler<int> OnCurrentCurrencyChanged;

        #endregion

        #region Private Methods

        private void Start()
        {
            PlayerCurrentCurrency = playerCurrentCurrency;
        }

        #endregion

        #region Public Methods

        public void ChangeCurrency(int amount)
        {
            PlayerCurrentCurrency -= amount;
            OnCurrentCurrencyChanged?.Invoke(this, PlayerCurrentCurrency);
        }

        #endregion
    }
}