using System;
using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;
using Features.Player.Scripts;

namespace Features.Currency.Scripts
{
    public class CurrencyProduct : MonoBehaviour, IProduct
    {
        #region Variables

        public string ProductName { get; }
        public bool IsInitialized { get; private set; }

        [SerializeField] private int currencyAmount = 5;
        [SerializeField] private float currencySpreadRadius = 2f;
        [SerializeField] private float spreadForce = 5f;
        [SerializeField] private float jumpHeight = 5f;
        [SerializeField] private float flySpeed = 5f;
        [SerializeField] private LayerMask playerLayer;

        private GameObject playerGameObject;
        private Rigidbody currencyRigidbody;
        private bool isReturned = false;

        #endregion

        #region Private Methods

        private void Awake()
        {
            currencyRigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            isReturned = false; // Reset when reused from pool
        }

        private void Start()
        {
            playerGameObject = GameObject.Find("Player");
        }

        private void FixedUpdate()
        {
            if (!IsInitialized)
            {
                return;
            }

            Vector3 targetPos = playerGameObject.transform.position;
            Vector3 direction = (targetPos - transform.position).normalized;
            HandleCurrencyMovement(direction);
        }

        private void HandleCurrencyMovement(Vector3 direction)
        {
            currencyRigidbody.MovePosition(currencyRigidbody.position + direction * (flySpeed * Time.fixedDeltaTime));
        }

        private Vector3 RandomizeDropPosition()
        {
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * currencySpreadRadius;
            Vector3 spawnPos = new Vector3(transform.position.x + randomOffset.x, transform.position.y, transform.position.z + randomOffset.y);
            return spawnPos;
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


        private void OnCollisionEnter(Collision other)
        {
            if (isReturned)
            {
                return;
            }

            if (((1 << other.gameObject.layer) & playerLayer) != 0)
            {
                playerGameObject.GetComponent<PlayerCurrency>().ChangeCurrency(currencyAmount, false);
                isReturned = true;
                ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            }
        }

        #endregion

        #region Interface Methods

        public void Initialize()
        {
            IsInitialized = true;
            InitializeDropMovement();
        }

        #endregion
    }
}