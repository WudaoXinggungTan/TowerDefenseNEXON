using UnityEngine;
using Features.Core.Scripts;

namespace Features.Tower.Scripts
{
    public class TowerSpawner : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject towerSpawnPosition;
        [SerializeField] private TowerFactory towerFactory;

        private CollisionDetector detector;
        private bool isSpawned = false;

        #endregion

        #region Private Methods

        private void Start()
        {
            detector = towerSpawnPosition.GetComponent<CollisionDetector>();
            if (detector != null)
            {
                detector.OnCollisionDetected += HandlePointCollision;
            }
        }

        private void HandlePointCollision(GameObject point, Collider other)
        {
            if (isSpawned)
            {
                return;
            }

            isSpawned = true;
            towerFactory.GetProduct(towerSpawnPosition.transform.position);
            detector.Destroy();
        }

        #endregion
    }
}