using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace Toy_1
{
    public class CharacterController : MonoBehaviour
    {

        [Header("Input Settings")]
        public PlayerInput playerInput;
        public float movementSmoothingSpeed = 1f;
        private Vector3 rawInputMovement;
        private Vector3 smoothInputMovement;

        //

        private Vector3 movementDirection;
        private Vector3 lastMoveDirection;

        public void Jump()
        {
            Debug.Log("Jump");
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            // rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
            rawInputMovement = new Vector3(inputMovement.x, inputMovement.y,0);
            Debug.Log(Vector3.Distance(Vector3.zero, rawInputMovement)
                + " and " + rawInputMovement);
            rawInputMovement.Normalize();
            if (Vector3.Distance(Vector3.zero, rawInputMovement) > 0)
            {
                lastMoveDirection = rawInputMovement;
            }
            
        }

        void CalculateMovementInputSmoothing()
        {

            smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);

        }
        void UpdatePlayerMovement()
        {
            UpdateMovementData(smoothInputMovement);
        }
        public void UpdateMovementData(Vector3 newMovementDirection)
        {
            movementDirection = newMovementDirection;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CalculateMovementInputSmoothing();
            UpdatePlayerMovement();

            HandleDash();


        }
        private void FixedUpdate()
        {
            // this.transform.position = this.transform.position + movementDirection;
            this.transform.position = this.transform.position + rawInputMovement;
        }

        private void HandleDash()
        {

        }

        public void OnDash()
        {
            // Vector2 inputMovement = value.ReadValue<Vector2>();
            float dashDistance = 5f;
            transform.position += lastMoveDirection * dashDistance;
        }

    }
}
