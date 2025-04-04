using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeCourse_Player : MonoBehaviour
{

    // [SerializeField] private gamei playerInput;

    private float movementSpeed = 5f;
    private float rotateSpeed = 5f;
    private Vector3 inputMovement;

    private GameInput gameInput;
    private bool isMoving;

    private void Awake()
    {
        gameInput = new GameInput();
        gameInput.Player.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = GetMovementVectorNormalized();
        transform.position += new Vector3(inputVector.x, 0, inputVector.y) * movementSpeed * Time.deltaTime;
        isMoving = inputVector != Vector2.zero;

        transform.forward = Vector3.Slerp(transform.forward, new Vector3(inputVector.x, 0, inputVector.y), Time.deltaTime * rotateSpeed);


    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = gameInput.Player.Move.ReadValue<Vector2>();
        // inputVector = inputVector.normalized;
        // -> normalized already from gameinput setting
        Debug.Log("inputVector: " + inputVector);
        return inputVector;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        
        Vector2 currentValue = value.ReadValue<Vector2>();
        inputMovement = new Vector3(currentValue.x, 0, currentValue.y);
    }

    public void MoveThePlayer()
    {
        Vector3 moveDirection = inputMovement.normalized;
        // Vector3 moveVelocity = moveDirection * 5f;
        Vector3 moveVelocity = moveDirection * movementSpeed;
        transform.position += moveVelocity * Time.deltaTime;


    }
    // fire related
    [SerializeField] private GameObject bulletPrefab;

    private void Shoot(Vector3 shootPosition, Vector3 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);
        Vector3 shootDirection = (targetPosition - shootPosition).normalized;

        bullet.GetComponent<FreeCourse_Projectile>().Setup(shootDirection);
    }

    public List<GameObject> targets = new List<GameObject>(8);
    private void CheckHit()
    {
        // foreach (Target target in Target.targetsList)
        foreach (GameObject target in targets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
            {
                // target.Damage();
                Destroy(gameObject);
            }
        }
    }

    // fire related end

}
