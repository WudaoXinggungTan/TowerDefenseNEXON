using System;
using UnityEngine;
using Features.Core.Scripts;

namespace Features.Enemy.Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        #region Variables

        public bool CanMove { get; private set; }

        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        private GameObject destination;
        private Rigidbody enemyRigidbody;
        private Quaternion rotation = Quaternion.identity;

        #endregion

        #region Private Methods

        private void Awake()
        {
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            destination = GameObject.Find("Destination");
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.IsGamePlaying())
            {
                return;
            }

            Vector3 targetPos = destination.transform.position;
            targetPos.y = transform.position.y; // Enemy always stays at the starting height, else it would fly up to reach the pivot point of the destination

            Vector3 direction = (targetPos - transform.position).normalized;
            HandleEnemyMovement(direction);
            HandleEnemyRotation(direction);
        }

        private void HandleEnemyMovement(Vector3 direction)
        {
            float gameSpeed = GameManager.Instance.GameSpeedMultiplier();
            enemyRigidbody.MovePosition(enemyRigidbody.position + direction * (moveSpeed * gameSpeed * Time.fixedDeltaTime));
        }

        private void HandleEnemyRotation(Vector3 direction)
        {
            Vector3 targetRotation = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.fixedDeltaTime, 0f);
            rotation = Quaternion.LookRotation(targetRotation);
            enemyRigidbody.MoveRotation(rotation);
        }

        #endregion
    }
}