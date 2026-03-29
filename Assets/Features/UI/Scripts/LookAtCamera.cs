using UnityEngine;

namespace Features.UI.Scripts
{
    public class LookAtCamera : MonoBehaviour
    {
        #region Variables

        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted,
        }

        [SerializeField] private Mode mode;

        #endregion

        #region Private Methods

        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case Mode.LookAtInverted:
                    Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -(Camera.main.transform.forward);
                    break;
            }
        }

        #endregion
    }
}