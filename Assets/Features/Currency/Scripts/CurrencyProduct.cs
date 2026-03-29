using System;
using UnityEngine;
using Features.Core.Scripts.Interface;

namespace Features.Currency.Scripts
{
    public class CurrencyProduct : MonoBehaviour, IProduct
    {
        #region Variables

        public string ProductName { get; }
        public bool IsInitialized { get; private set; }

        [SerializeField] private float currencySpreadRadius = 2f;
        [SerializeField] private float spreadForce = 5f;
        [SerializeField] private float jumpHeight = 5f;
        private Rigidbody currencyRigidbody;

        #endregion

        #region Private Methods

        private void Awake()
        {
            currencyRigidbody = GetComponent<Rigidbody>();
        }

        private Vector3 RandomizeDropPosition()
        {
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * currencySpreadRadius;
            Vector3 spawnPos = new Vector3(transform.position.x + randomOffset.x, transform.position.y, transform.position.z + randomOffset.y);
            return spawnPos;
        }

        #endregion

        #region Interface Methods

        public void Initialize()
        {
            IsInitialized = true;

            InitializeDropMovement();
        }

        private void InitializeDropMovement()
        {
            Vector3 targetPosition = RandomizeDropPosition();
            Vector3 direction = (targetPosition - transform.position);

            direction += Vector3.up * jumpHeight;
            currencyRigidbody.linearVelocity = Vector3.zero;
            currencyRigidbody.angularVelocity = Vector3.zero;
            currencyRigidbody.AddForce(direction * spreadForce, ForceMode.Impulse);
        }

        #endregion
    }
}