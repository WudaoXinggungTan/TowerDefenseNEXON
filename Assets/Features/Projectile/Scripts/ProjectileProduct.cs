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
        private bool isReleased = false;

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
            isReleased = false;
        }

        public void FixedUpdate()
        {
            if (Target == null || !Target.activeInHierarchy)
            {
                ReturnToObjectPool();
                return;
            }

            Vector3 targetPosition = Target.transform.position;
            Vector3 direction = (targetPosition - transform.position).normalized;

            projectileRigidbody.MovePosition(projectileRigidbody.position + direction * (speed * Time.fixedDeltaTime));
            if (targetPosition != projectileRigidbody.position)
            {
                transform.LookAt(targetPosition);
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
            if (isReleased)
            {
                return;
            }

            isReleased = true;
            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
        }

        #endregion
    }
}