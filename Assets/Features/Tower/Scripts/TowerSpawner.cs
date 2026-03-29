using UnityEngine;
using Features.Core.Scripts;
using Features.Player.Scripts;

namespace Features.Tower.Scripts
{
    public class TowerSpawner : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject towerSpawnPosition;
        [SerializeField] private TowerFactory towerFactory;
        [SerializeField] private int requireCurrency;

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

            int playerCurrency = other.GetComponent<PlayerCurrency>().PlayerCurrentCurrency;

            if (playerCurrency < requireCurrency)
            {
                return;
            }

            other.GetComponent<PlayerCurrency>().ChangeCurrency(requireCurrency);

            isSpawned = true;
            towerFactory.GetProduct(towerSpawnPosition.transform.position);
            detector.Destroy();
        }

        #endregion
    }
}