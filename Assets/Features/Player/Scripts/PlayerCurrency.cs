using System;
using System.Collections;
using UnityEngine;

namespace Features.Player.Scripts
{
    public class PlayerCurrency : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int playerInitialCurrency;

        public int PlayerCurrentCurrency { get; private set; }
        public static event EventHandler<int> OnCurrentCurrencyChanged;

        #endregion

        #region Private Methods

        private void Start()
        {
            PlayerCurrentCurrency = playerInitialCurrency;
        }

        #endregion

        #region Public Methods

        public void ChangeCurrency(int amount, bool lower = true)
        {
            StartCoroutine(ChangeCurrencyRoutine(amount, lower));
        }

        private IEnumerator ChangeCurrencyRoutine(int amount, bool lower)
        {
            int count = 0;

            while (count < amount)
            {
                count++;

                if (lower)
                {
                    PlayerCurrentCurrency -= 1;
                }
                else
                {
                    PlayerCurrentCurrency += 1;
                }
                OnCurrentCurrencyChanged?.Invoke(this, PlayerCurrentCurrency);
                yield return new WaitForSeconds(0.01f);
            }
        }

        #endregion
    }
}