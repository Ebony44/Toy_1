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

    public float turnSpeed = 2f;

    public Vector3 movementDirection;

    public Rigidbody playerRB;
    private Ray shootRay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovementInputSmoothing();
        CalculateMoveDirection(smoothInputMovement);

        TestDrawLineAtLookDir();

    }
    private void FixedUpdate()
    {
        
        MoveThePlayer();
        TurnThePlayer();
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
        // Debug.Log("smooth movement is " + smoothInputMovement.ToString()
        //     + " raw movement is " + rawInputMovement);
        

    }

    private void CalculateMoveDirection(Vector3 newDirection)
    {
        Debug.Log("[CalculateMoveDirection], dir is " + newDirection);
        movementDirection = newDirection;
    }
    void MoveThePlayer()
    {
        #region 
        // Vector3 movement = smoothInputMovement * movementSpeed * Time.deltaTime;
        // playerRB.MovePosition(transform.position + movement);

        // Vector3 movement = inputMovement * movementSpeed * Time.deltaTime;
        // playerRB.MovePosition(transform.position + movement);
        #endregion

        Vector3 movement = smoothInputMovement.ToIso() * movementSpeed * Time.deltaTime;
        playerRB.MovePosition(transform.position + movement);


    }

    private void TurnThePlayer()
    {
        if(movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion newRot = Quaternion.Lerp(
                playerRB.rotation,
                Quaternion.LookRotation(movementDirection),
                t: turnSpeed
                );
            playerRB.MoveRotation(newRot);
        }
    }

    // private void OnGUI() {
    //     // Debug.DrawRay()
    //     // shootRay.origin = transform.position;
    //     // shootRay.direction = transform.forward;

    //     TestDrawLineAtLookDir();

    // }

    public void TestDrawLineAtLookDir()
   {
      Vector3 start = transform.position + Vector3.up;
      Vector3 dir = transform.forward * 35f;
      Debug.DrawRay(start, dir, Color.red);
      Debug.Log("start is " + start
      + " dir is " + dir);
   }

}
