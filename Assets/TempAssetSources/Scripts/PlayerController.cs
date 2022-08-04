using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 inputMovement;
    private Vector3 smoothInputMovement;

    public float inputMovementSmoothingSpeed = 1f;

    public Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MoveThePlayer();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        inputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
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

    void CalculateMovementInputSmoothing()
    {

        smoothInputMovement = Vector3.Lerp(smoothInputMovement, inputMovement, Time.deltaTime * inputMovementSmoothingSpeed);

    }
    void MoveThePlayer()
    {
        playerRB.MovePosition(smoothInputMovement);
    }

}
