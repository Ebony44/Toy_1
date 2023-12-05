using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject tempTestObj;
    private Camera cam;


    private void Start()
    {
        cam = Camera.main;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("[OnBeginDrag]");
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        this.transform.position = eventData.position;
        Vector2 mousepos = new Vector2();
        Vector3 point = new Vector3();
        // mousepos.x = currentEvent.mousePosition.x;
        // mousepos.y = currentEvent.mousePosition.y;

        

        mousepos.x = Mouse.current.position.x.ReadValue();
        mousepos.y = Mouse.current.position.y.ReadValue();
        point = cam.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, cam.nearClipPlane));
        Debug.Log("mouse pos is " + mousepos
            + " point is " + point);
        tempTestObj.transform.position = point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("[OnEndDrag]");
    }
}
