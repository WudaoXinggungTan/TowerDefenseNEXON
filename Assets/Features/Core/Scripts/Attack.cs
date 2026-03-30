using System.Collections.Generic;
using UnityEngine;

namespace Features.Core.Scripts
{
    [RequireComponent(typeof(GameObjectDetector))]
    public abstract class Attack : MonoBehaviour
    {
        #region Variables

        [SerializeField] protected Factory projectileFactory;
        [SerializeField] protected Transform projectileFirePosition;
        [SerializeField] protected float attackRate = 1f;

        protected GameObject CurrentTarget;
        private GameObjectDetector gameObjectDetector;
        private float nextFireTime = 0f;

        private float nearestDistanceToTarget;
        private float distanceToTarget;

        #endregion

        #region Private Methods

        private void Start()
        {
            gameObjectDetector = GetComponent<GameObjectDetector>();
            gameObjectDetector.OnGameObjectsDetected += HandleOnGameObjectsDetected;
        }

        private void HandleOnGameObjectsDetected(List<GameObject> detectedGameObjectList)
        {
            if (detectedGameObjectList.Count < 1)
            {
                CurrentTarget = null;
                return;
            }

            if (CurrentTarget != null)
            {
                if (!CurrentTarget.activeInHierarchy || !detectedGameObjectList.Contains(CurrentTarget))
                {
                    CurrentTarget = null; // Target walked out of range or died
                }
            }
            else
            {
                nearestDistanceToTarget = float.MaxValue;
                int count = 0;
                int maximumFinds = 4;
                foreach (GameObject target in detectedGameObjectList)
                {
                    count++;
                    distanceToTarget = (transform.position - target.transform.position).sqrMagnitude;
                    if (distanceToTarget < nearestDistanceToTarget)
                    {
                        nearestDistanceToTarget = distanceToTarget;
                        CurrentTarget = target;
                    }

                    // Even if there are 1000 objects in the radius, only loop this 4 times maximum
                    if (count >= maximumFinds)
                    {
                        break;
                    }
                }
            }
        }

        private void Update()
        {
            if (!GameManager.Instance.IsGamePlaying())
            {
                return;
            }

            HandleAttack();
        }

        private void HandleAttack()
        {
            if (CurrentTarget != null && Time.time >= nextFireTime)
            {
                Attacking();
                // If fireRate is 1 -> attacks every second, 2 -> attacks every 0.5 seconds.
                nextFireTime = Time.time + (1f / attackRate);
            }
        }

        private void OnDisable()
        {
            CurrentTarget = null;
        }

        private void OnDestroy()
        {
            if (gameObjectDetector != null)
                gameObjectDetector.OnGameObjectsDetected -= HandleOnGameObjectsDetected;
        }

        #endregion

        #region Protected Methods

        protected abstract void Attacking();

        #endregion
    }
}