using System;
using UnityEngine;

namespace Features.Player.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput playerInput;
        private Rigidbody rigidbody;
        public float moveSpeed = 10f;

        private Vector2 moveInputDirection;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            playerInput.Move += (direction) => { moveInputDirection = direction; };
        }

        private void Update()
        {
            HandlePlayerMovement();
        }

        private void HandlePlayerMovement()
        {
            float moveDistance = Time.deltaTime * moveSpeed;


            //transform.Translate(moveDistance * moveInputDirection);
            rigidbody.linearVelocity = moveInputDirection * moveSpeed;
        }
    }
}