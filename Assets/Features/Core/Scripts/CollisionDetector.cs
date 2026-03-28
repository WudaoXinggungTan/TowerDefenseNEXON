using System;
using UnityEngine;

namespace Features.Core.Scripts
{
    public class CollisionDetector : MonoBehaviour
    {
        #region Variables

        public event Action<GameObject, Collider> OnCollisionDetected;

        [SerializeField] private LayerMask layerMask;

        public LayerMask LayerMask
        {
            get => layerMask;
            set => layerMask = value;
        }

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & layerMask) != 0)
            {
                OnCollisionDetected?.Invoke(this.gameObject, other);
            }
        }

        #endregion

        #region Public Methods

        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}