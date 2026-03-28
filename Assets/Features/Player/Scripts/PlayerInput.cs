using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

namespace Features.Player.Scripts
{
    public class PlayerInput : MonoBehaviour, PlayerInputSystem_Actions.IPlayerActions, PlayerInputSystem_Actions.IUIActions
    {
        #region Variables

        public event UnityAction<Vector2> Move = delegate { };
        public Vector2 Direction => playerInputSystem.Player.Move.ReadValue<Vector2>();

        private PlayerInputSystem_Actions playerInputSystem;

        #endregion

        #region Private Methods

        private void Start()
        {
            /*GameManager.Instance.OnGameStateChanged += (_sender, _args) =>
            {
                if (GameManager.Instance.IsGamePlaying())
                {
                    EnablePlayerAction();
                }
            };*/
        }

        private void OnEnable()
        {
            EnablePlayerAction();
            SetUIInputAndPlayerInputReference();
        }

        private void OnDestroy()
        {
            DisablePlayerAction();
        }

        private void EnablePlayerAction()
        {
            if (playerInputSystem == null)
            {
                playerInputSystem = new PlayerInputSystem_Actions();
                //SetCallbacks those two for interface methods working
                playerInputSystem.Player.SetCallbacks(this);
                playerInputSystem.UI.SetCallbacks(this);
            }

            playerInputSystem.Enable();
        }

        private void DisablePlayerAction()
        {
            if (playerInputSystem == null)
            {
                return;
            }

            playerInputSystem.Disable();
        }

        private void SetUIInputAndPlayerInputReference()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null)
            {
                Debug.LogWarning("No Event System found in scene");
                return;
            }

            var uiModule = eventSystem.GetComponent<InputSystemUIInputModule>();
            if (uiModule == null)
            {
                Debug.LogWarning("No InputSystemUIInputModule found in scene");
                return;
            }

            if (uiModule.actionsAsset != playerInputSystem.asset)
            {
                // Make sure they both reference the same asset;
                uiModule.actionsAsset = playerInputSystem.asset;

                Debug.Log("Successfully assigned inputAction.asset to InputSystemUIInputModule.");
            }
        }

        #endregion

        #region IPlayerActions

        // ----- IPlayerActions-----

        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context) { }

        public void OnAttack(InputAction.CallbackContext context)
        {
        }

        public void OnInteract(InputAction.CallbackContext context) { }

        public void OnCrouch(InputAction.CallbackContext context) { }

        public void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    //
                    break;
                case InputActionPhase.Canceled:
                    //
                    break;
            }
        }

        public void OnPrevious(InputAction.CallbackContext context) { }

        public void OnNext(InputAction.CallbackContext context) { }

        public void OnSprint(InputAction.CallbackContext context) { }

        #endregion

        #region IUIActions

        // ----- IUIActions-----

        public void OnNavigate(InputAction.CallbackContext context) { }

        public void OnSubmit(InputAction.CallbackContext context) { }

        public void OnCancel(InputAction.CallbackContext context) { }

        public void OnPoint(InputAction.CallbackContext context) { }

        public void OnClick(InputAction.CallbackContext context) { }

        public void OnRightClick(InputAction.CallbackContext context) { }

        public void OnMiddleClick(InputAction.CallbackContext context) { }

        public void OnScrollWheel(InputAction.CallbackContext context) { }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }

        #endregion
    }
}