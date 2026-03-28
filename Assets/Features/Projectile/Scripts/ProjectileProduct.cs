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
        [SerializeField] private float arcHeight = 5f;

        public string ProductName { get; }
        public bool IsInitialized { get; private set; }
        public GameObject Target { get; set; }

        private Rigidbody projectileRigidbody;
        private float traveledTime;

        #endregion

        #region Public Methods

        public void Initialize()
        {
            IsInitialized = true;
            projectileRigidbody = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            if (Target != null)
            {
                traveledTime += Time.fixedDeltaTime * speed;
                float t = Mathf.Clamp01(traveledTime);
                Vector3 nextPosition = MathParabola.Parabola(transform.position, Target.transform.position, arcHeight, t);
                projectileRigidbody.MovePosition(nextPosition);
            }
        }

        private void HandleImpact()
        {
            // Logic for hitting the enemy, then returning to pool
            // ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
        }

        #endregion
    }
}