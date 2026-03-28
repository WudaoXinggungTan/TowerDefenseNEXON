using UnityEngine;
using Features.Core.Scripts.Interface;

namespace Features.Enemy.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyProduct : MonoBehaviour, IProduct
    {
        #region Variables

        public string ProductName { get; }
        public bool IsInitialized { get; private set; }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            IsInitialized = true;
        }

        #endregion
    }
}