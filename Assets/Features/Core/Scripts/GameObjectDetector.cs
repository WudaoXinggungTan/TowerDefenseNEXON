using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Features.Core.Scripts
{
    public class GameObjectDetector : MonoBehaviour
    {
        #region Variables

        [SerializeField] private LayerMask detectionLayer;
        [SerializeField] private float detectionRadius = 10f;
        [SerializeField] private float detectionInterval = 0.25f; // Roughly 15 frames at 60 FPS

        private HashSet<GameObject> gameObjects;

        public event Action<HashSet<GameObject>> OnGameObjectsDetected;

        #endregion

        #region Private Methods

        private void Awake()
        {
            gameObjects = new HashSet<GameObject>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(DetectObjects), 0f, detectionInterval);
        }

        // Using OverlapSphere to detect all the game object currently inside the radius each 15 frames.
        private void DetectObjects()
        {
            gameObjects.Clear();

            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

            foreach (Collider col in colliders)
            {
                if (((1 << col.gameObject.layer) & detectionLayer) != 0)
                {
                    gameObjects.Add(col.gameObject);
                }
            }
            
            OnGameObjectsDetected?.Invoke(gameObjects);
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