using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

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

            if(Keyboard.current.xKey.wasPressedThisFrame)
            {   
                LookAtPointer(GetMousePositionInNewInput());
            }
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                Fire(GetMousePositionInNewInput());
            }


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

        public void LookAtPointer(Vector3 pointerPos)
        {
            var relativePos = pointerPos - this.transform.position;
            Debug.Log("pos is " + relativePos);
            // var newRot = Quaternion.LookRotation(relativePos, Vector3.forward);
            var newRot = Quaternion.LookRotation( Vector3.forward,relativePos); // actual heading
            // var newRot = Quaternion.LookRotation(relativePos, Vector3.back);
            this.transform.rotation = Quaternion.Euler(0,0,newRot.eulerAngles.z); // 2d will rotate around z-axis.
        }
        public Vector3 GetMousePositionInNewInput()
        {

            // Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position);
            Vector3 readValue = Mouse.current.position.ReadValue();
            // Vector3 paramWorldPos = new Vector3(readValue.x, readValue.y, Camera.main.transform.position.z);
            Vector3 cameraPos = Camera.main.transform.position;
            float cameraPosModZ = 0;
            if(cameraPos.z < 0)
            {
                cameraPosModZ = -cameraPos.z;
            }
            else
            {
                cameraPosModZ = cameraPos.z;
            }

            Vector3 paramWorldPos = new Vector3(readValue.x, readValue.y, cameraPosModZ);
            Vector3 pos = Camera.main.ScreenToViewportPoint(readValue);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(paramWorldPos);
            Debug.Log("mouse pos is " + pos
                + " world pos is " + worldPos
                + " read value is " + readValue
                + " param world pos " + paramWorldPos);

            return worldPos;
        }


        #region shoot related, seperate them lately
        public GameObject bulletPrefab;
        public Transform firePoint;
        public float bulletSpeed = 20f;
        public void Fire(Vector3 direction)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        }
        private IEnumerator Wait(float waitSecond, Action callback)
        {
            yield return (new WaitForSeconds(waitSecond));
            callback?.Invoke();

        }
        #endregion



    }
}
