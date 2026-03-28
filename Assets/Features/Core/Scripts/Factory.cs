using UnityEngine;

namespace Features.Core.Scripts
{
    public abstract class Factory : MonoBehaviour
    {
        #region Public Methods

        public abstract Interface.IProduct GetProduct(Vector3 position);

        #endregion
    }
}