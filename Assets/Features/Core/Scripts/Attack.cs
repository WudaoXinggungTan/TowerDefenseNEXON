using System.Collections.Generic;
using UnityEngine;
using Features.Core.Scripts.Interface;

namespace Features.Core.Scripts
{
    public abstract class Attack : MonoBehaviour
    {
        #region Variables

        [SerializeField] protected Factory projectileFactory;
        [SerializeField] protected Transform projectileFirePosition;
        [SerializeField] protected float attackRate = 1f;

        protected List<GameObject> TargetList;
        protected GameObject CurrentTarget;

        private GameObjectDetector gameObjectDetector;
        private float nextFireTime = 0f;

        #endregion

        #region Private Methods

        private void Start()
        {
            gameObjectDetector = GetComponent<GameObjectDetector>();
            gameObjectDetector.OnGameObjectsDetected += HandleTargetsDetected;
        }

        private void HandleTargetsDetected(HashSet<GameObject> detectedObjects)
        {
            if (detectedObjects.Count < 1)
            {
                CurrentTarget = null;
                return;
            }

            if (CurrentTarget == null || !CurrentTarget.activeInHierarchy)
            {
                foreach (GameObject obj in detectedObjects)
                {
                    CurrentTarget = obj;
                    break;
                }
            }
        }

        private void Update()
        {
            if (CurrentTarget && Time.time >= nextFireTime)
            {
                Attacking();
                // If fireRate is 1 -> attacks every second, 2 -> attacks every 0.5 seconds.
                nextFireTime = Time.time + (1f / attackRate);
            }
        }

        private void OnDestroy()
        {
            if (gameObjectDetector != null)
                gameObjectDetector.OnGameObjectsDetected -= HandleTargetsDetected;
        }

        #endregion

        #region Protected Methods

        protected abstract void Attacking();

        #endregion
    }
}