using UnityEngine;

namespace Toy_1
{
    public class ManagementCameraManager : MonoBehaviour
    {
        private float movementSpeed = 5f;
        private float rotateSpeed = 5f;
        private Vector3 inputMovement;

        private GameInput gameInput;
        private bool isMoving;

        public float zoomSpeed = 5f;
        public float minimumZoomLimit = 0f;
        public float maximumZoomLimit = 10f;

        private void Awake()
        {
            gameInput = new GameInput();
            // gameInput.
            gameInput.ManagePhaseInput.Enable();
            gameInput.Player.Disable();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector2 inputMovementVector = GetMovementVectorNormalized();
            
            //transform.position += new Vector3(inputMovementVector.x, 0, inputMovementVector.y) * movementSpeed * Time.deltaTime;
            //isMoving = inputMovementVector != Vector2.zero;

            //transform.forward = Vector3.Slerp(transform.forward, new Vector3(inputMovementVector.x, 0, inputMovementVector.y), Time.deltaTime * rotateSpeed);
            

            // Vector2 
            Vector2 inputWheelVector = GetMouseWheelZoom();
            float currentZoomValue = inputWheelVector.y * zoomSpeed * Time.deltaTime;
            // currentZoomValue = Mathf.Clamp(currentZoomValue, minimumZoomLimit, maximumZoomLimit);
            // transform.position
            transform.position += new Vector3(0, 0, currentZoomValue);
            if(transform.position.z >= maximumZoomLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maximumZoomLimit);
            }
            else if (transform.position.z <= minimumZoomLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minimumZoomLimit);
            }
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = gameInput.Player.Move.ReadValue<Vector2>();
            // inputVector = inputVector.normalized;
            // -> normalized already from gameinput setting
            // Debug.Log("[GetMovementVectorNormalized], inputVector: " + inputVector);
            return inputVector;
        }

        public Vector2 GetMouseWheelZoom()
        {
               Vector2 inputVector = gameInput.ManagePhaseInput.MouseWheel.ReadValue<Vector2>();
            // inputVector = inputVector.normalized;
            // -> normalized already from gameinput setting
            // Debug.Log("[GetMouseWheelZoom], inputVector: " + inputVector);
            return inputVector;
        }


    }

}
