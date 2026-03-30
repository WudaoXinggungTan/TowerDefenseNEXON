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
            Show();
            PlayerCurrency.OnCurrentCurrencyChanged += (sender, currency) =>
            {
                UpdateCurrencyText(currency);
            };
        }

        private void UpdateCurrencyText(int currency)
        {
            currencyText.text = Convert.ToString(currency);
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