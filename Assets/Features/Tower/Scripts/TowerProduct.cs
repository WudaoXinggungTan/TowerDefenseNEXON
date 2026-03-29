using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;

namespace Features.Tower.Scripts
{
    public class TowerProduct : MonoBehaviour, IProduct
    {
        #region Variabels

        public string ProductName { get; }
        public bool IsInitialized { get; }

        public Vector3 TowerPositon { get; private set; }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            TowerPositon = transform.position;
        }

        #endregion
    }
}