using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommonCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestRoutine());
    }

    private WaitForSeconds cachedWaitForSeconds = new WaitForSeconds(1f);
    public IEnumerator TestRoutine()
    {
        int iterations = 0;
        int maxIterations = 10;
        while (iterations < maxIterations)
        {
            Debug.Log("Iteration: " + iterations);
            iterations++;
            yield return cachedWaitForSeconds;
        }
    }

    public GameObject cameraObject;
    public Vector3 offset = new Vector3(0, 0, 1);
    public Transform target;

    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float zoomSpeed = 4f;

    private float currentZoom = 10f;
    private PlayerInput playerInput;
    private Vector2 scrollInput;

    void Awake()
    {
        // playerInput = new PlayerInput();
        // playerInput.Camera.Zoom.performed += ctx => scrollInput = ctx.ReadValue<Vector2>();

    }

    void OnEnable()
    {
        // playerInput.ActivateInput();
        
    }

    void OnDisable()
    {
        // playerInput.DeactivateInput();
        // playerInput.Disable();
    }

    public void OnMouseWheelScrollChanged(InputAction.CallbackContext callbackContext)
    {
        float currentAxis = callbackContext.ReadValue<float>();
        Debug.Log("MouseWheelScrollChanged: " + currentAxis);
        scrollInput.y = currentAxis > 0 ? 1 
            : currentAxis < 0 ? -1 
            : 0;
    }

    void Update()
    {
        
        currentZoom -= scrollInput.y * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        //currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    void LateUpdate()
    {
        if (cameraObject == null)
        {
            return;
        }
        cameraObject.transform.position = target.position - offset * currentZoom;
    }
}
