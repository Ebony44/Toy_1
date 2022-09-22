using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestNavMeshController : MonoBehaviour
{
    [SerializeField]
    private Camera Camera = null;
    private ObstacleAgent Agent;

    private RaycastHit[] Hits = new RaycastHit[1];

    private void Awake()
    {
        Agent = GetComponent<ObstacleAgent>();
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
        var tempVector = value.ReadValue<Vector2>();
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.RaycastNonAlloc(ray, Hits) > 0)
        {
            Agent.SetDestination(Hits[0].point);
        }

    }
}
