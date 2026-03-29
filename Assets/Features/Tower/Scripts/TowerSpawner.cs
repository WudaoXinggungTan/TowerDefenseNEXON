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
                detector.OnCollisionDetected += HandlePlayerCollision;
            }
        }

        private void HandlePlayerCollision(GameObject point, Collider other)
        {
            if (isSpawned)
            {
                return;
            }

            PlayerCurrency playerCurrency = other.GetComponent<PlayerCurrency>();

            int playerCurrentCurrency = playerCurrency.PlayerCurrentCurrency;
            if (playerCurrentCurrency < requireCurrency)
            {
                return;
            }

            playerCurrency.ChangeCurrency(requireCurrency);

            isSpawned = true;
            towerFactory.GetProduct(towerSpawnPosition.transform.position);
            detector.Destroy();
        }

        #endregion
    }
}