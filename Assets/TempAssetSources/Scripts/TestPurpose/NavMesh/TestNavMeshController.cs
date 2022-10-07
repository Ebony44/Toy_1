using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestNavMeshController : MonoBehaviour
{
    [SerializeField]
    private Camera Camera = null;
    [SerializeField]
    private ObstacleAgent Agent;

    private RaycastHit[] Hits = new RaycastHit[1];

    [SerializeField] private InputActionAsset inputMap;

    

    InputAction click;
    InputAction pos;

    private void Awake()
    {
        inputMap.Enable();
        Agent = GetComponent<ObstacleAgent>();
        // playerInput = new PlayerInput();
        click = inputMap.FindAction("Fire");
        pos = inputMap.FindAction("MousePosition");

        click.performed += Process;

    }
    private void OnEnable()
    {
        // inputMap.Enable();
        
    }

    public void Process(InputAction.CallbackContext value)
    {
        Debug.Log($"Input action: {pos.ReadValue<Vector2>()}");

        Vector3 tempVector = pos.ReadValue<Vector2>();
        tempVector.z = Camera.main.farClipPlane * 0.5f;
        Ray ray = Camera.ScreenPointToRay(tempVector);
        var temp = Physics.RaycastNonAlloc(ray, Hits);
        var temp2 = Hits[0].point;

        if (Physics.RaycastNonAlloc(ray, Hits) > 0)
        {
            Agent.SetDestination(Hits[0].point);
        }

    }

    //private void Update()
    //{


    //    if (Input.GetKeyUp(KeyCode.Mouse0))
    //    {
    //        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

    //        if (Physics.RaycastNonAlloc(ray, Hits) > 0)
    //        {
    //            Agent.SetDestination(Hits[0].point);
    //        }
    //    }
    //}

    public void OnMouseClick(InputAction.CallbackContext value)
    {
        var temp = value;
        Debug.Log("value is " + value);

        // var tempVector = mouse.valueType

        // var mousePosition = PlayerInput.ControlsChangedEvent
        // var mousePositionZ = Camera.farClipPlane * .5f;
        // var mouseViewportPosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePositionZ));


        // var tempVector = value.ReadValue<Vector2>();

        // Vector3 tempVector = Mouse.current.position.ReadValue();



        // tempVector.z = Camera.main.farClipPlane * 0.5f;
        // Ray ray = Camera.ScreenPointToRay(tempVector);
        // 
        // if (Physics.RaycastNonAlloc(ray, Hits) > 0)
        // {
        //     Agent.SetDestination(Hits[0].point);
        // }

    }
}
