using System;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;
using UnityEngine;

namespace Features.Projectile.Scripts
{
    public class ProjectileProduct : MonoBehaviour, IProduct
    {
        #region Variables

        [SerializeField] private float speed = 2f;
        [SerializeField] private float projectileDamage = 5f;
        [SerializeField] private LayerMask targetLayerMask;

        public string ProductName { get; }
        public bool IsInitialized { get; private set; }
        public GameObject Target { get; set; }

        private Rigidbody projectileRigidbody;

        #endregion

        #region Private Methods

        private void Awake()
        {
            projectileRigidbody = GetComponent<Rigidbody>();
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void FixedUpdate()
        {
            if (Target == null)
            {
                return;
            }

            if (Target.gameObject.activeInHierarchy)
            {
                Vector3 targetPosition = Target.transform.position;
                Vector3 direction = (targetPosition - transform.position).normalized;

                projectileRigidbody.MovePosition(projectileRigidbody.position + direction * (speed * Time.fixedDeltaTime));
                if (targetPosition != projectileRigidbody.position)
                {
                    transform.LookAt(targetPosition);
                }
            }
            else
            {
                // If the target already died, return this (still flying) projectile to the pool
                ReturnToObjectPool();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & targetLayerMask) != 0)
            {
                HandleImpact(other.gameObject);
            }
        }

        private void HandleImpact(GameObject other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.Damage(projectileDamage);
            }

            ReturnToObjectPool();
        }

        private void ReturnToObjectPool()
        {
            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
        }

        #endregion
    }
}