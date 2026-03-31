using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Features.Player.Scripts;

namespace Features.UI.Scripts
{
    public class CurrencyUI : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Image currencyImage;
        [SerializeField] private TextMeshProUGUI currencyText;

        #endregion

        #region Private Methods

        private void Start()
        {
            PlayerCurrency.OnCurrentCurrencyChanged += PlayerCurrency_OnCurrentCurrencyChanged;
        }

        private void PlayerCurrency_OnCurrentCurrencyChanged(object sender, int currency)
        {
            UpdateCurrencyText(currency);
        }

        private void OnDisable()
        {
            PlayerCurrency.OnCurrentCurrencyChanged -= PlayerCurrency_OnCurrentCurrencyChanged;
        }

        private void UpdateCurrencyText(int currency)
        {
            currencyText.text = Convert.ToString(currency);
        }

        #endregion
    }
}