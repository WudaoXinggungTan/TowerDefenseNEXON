using System;
using UnityEngine;
using Features.Core.Scripts;

namespace Features.Player.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform mainCamera;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rotationSpeed = 10f;

        private PlayerInput playerInput;
        private Rigidbody playerRigidbody;

        private Vector2 moveInputDirection;
        private Quaternion rotation = Quaternion.identity;

        #endregion

        #region Private Methods

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            playerInput.Move += PlayerInputOnMove;
        }

        private void PlayerInputOnMove(Vector2 direction)
        {
            moveInputDirection = direction;
        }

        private void OnDestroy()
        {
            playerInput.Move -= PlayerInputOnMove;
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.IsGamePlaying())
            {
                return;
            }

            Vector3 moveDirection = CalculateMovementDirectionBaseOnCamera();
            Vector3 movementVector = new Vector3(moveDirection.x, 0f, moveDirection.z).normalized;
            if (movementVector.sqrMagnitude > 0.001f)
            {
                HandlePlayerMovement(movementVector);
                HandlePlayerRotation(movementVector);
            }
        }

        private Vector3 CalculateMovementDirectionBaseOnCamera()
        {
            Vector3 forward = mainCamera.forward;
            Vector3 right = mainCamera.right;

            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 forwardRelative = moveInputDirection.y * forward;
            Vector3 rightRelative = moveInputDirection.x * right;

            Vector3 moveDirection = forwardRelative + rightRelative;
            return moveDirection;
        }


        private void HandlePlayerMovement(Vector3 movementVector)
        {
            playerRigidbody.MovePosition(playerRigidbody.position + movementVector * (moveSpeed * Time.fixedDeltaTime));
        }


        private void HandlePlayerRotation(Vector3 movementVector)
        {
            Vector3 targetRotation = Vector3.RotateTowards(transform.forward, movementVector, rotationSpeed * Time.fixedDeltaTime, 0f);
            rotation = Quaternion.LookRotation(targetRotation);
            playerRigidbody.MoveRotation(rotation);
        }

        #endregion
    }
}