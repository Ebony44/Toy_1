using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;

    public float inputMovementSmoothingSpeed = 1f;
    public float movementSpeed = 2f;

    public Vector3 movementDirection;

    public Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovementInputSmoothing();
    }
    private void FixedUpdate()
    {
        
        MoveThePlayer();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        Debug.Log(value.ReadValue<Vector2>());
        if (value.performed)
        {
            Debug.Log("value performed " + value);
            // value.control.ToString();
            // value.control.
        }
        else if (value.started)
        {
            Debug.Log("value started" + value);
        }
        
    }
    [TestMethod(false)]
    void CalculateMovementInputSmoothing()
    {

        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * inputMovementSmoothingSpeed);
        Debug.Log("smooth movement is " + smoothInputMovement.ToString()
            + " raw movement is " + rawInputMovement);
        

    }
    void MoveThePlayer()
    {
        Vector3 movement = smoothInputMovement * movementSpeed * Time.deltaTime;
        playerRB.MovePosition(transform.position + movement);

        // Vector3 movement = inputMovement * movementSpeed * Time.deltaTime;
        // playerRB.MovePosition(transform.position + movement);
    }

}
