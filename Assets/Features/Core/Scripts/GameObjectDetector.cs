using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Core.Scripts
{
    public class GameObjectDetector : MonoBehaviour
    {
        #region Variables

        [SerializeField] private LayerMask detectionLayer;
        [SerializeField] private float detectionRadius = 10f;
        [SerializeField] private float detectionInterval = 0.25f; // Roughly 15 frames at 60 FPS

        private List<GameObject> detectedGameObjectList;

        public event Action<List<GameObject>> OnGameObjectsDetected;

        #endregion

        #region Private Methods

        private void Awake()
        {
            detectedGameObjectList = new List<GameObject>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(DetectObjects), 0f, detectionInterval);
        }

        // Using OverlapSphere to detect all the game object currently inside the radius each 15 frames.
        private void DetectObjects()
        {
            if (!GameManager.Instance.IsGamePlaying())
            {
                return;
            }

            detectedGameObjectList.Clear();

            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

            foreach (Collider col in colliders)
            {
                if (((1 << col.gameObject.layer) & detectionLayer) != 0)
                {
                    detectedGameObjectList.Add(col.gameObject);
                }
            }

            OnGameObjectsDetected?.Invoke(detectedGameObjectList);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(DetectObjects));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }

        #endregion
    }
}