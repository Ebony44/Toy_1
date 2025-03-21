using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject tempTestObj;
    private Camera cam;

    private Plane tempGround;

    public UnitDeployChecker deployChecker;
    public Action<bool> onCollided;
    public Material currentDragMat;
    // public BoxCollider unitLocateCollider; // if unit is more than 1, calculate it's size?

    // public delegate void OnCollideSomething();
    // public event OnCollideSomething onCollided;


    private void OnEnable()
    {
        // onCollided += deployChecker.CheckUnitDeploy;
        deployChecker.onCollided += DoUnitMaterialChanging;
    }
    private void OnDisable()
    {
        // onCollided -= deployChecker.CheckUnitDeploy;
        deployChecker.onCollided -= DoUnitMaterialChanging;
        ChangeDragMatColor(Color.white);
    }

    private void ChangeDragMatColor(Color paramColor)
    {
        if (currentDragMat != null)
        {
            // currentDragMat.color = bIsDeployable == true ? Color.green : Color.red;
            currentDragMat.color = paramColor;
        }
    }

    public void DoUnitMaterialChanging(bool bIsDeployable)
    {
        Debug.Log("[DoUnitMaterialChanging], changing units' texture, which indicates they can be deployed or not" +
            " currently " + bIsDeployable);
        var currentColor = bIsDeployable == true ? Color.green : Color.red;
        ChangeDragMatColor(currentColor);

    }

    private void Start()
    {
        cam = Camera.main;
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("[OnBeginDrag]");
        // tempGround = new Plane(Vector3.forward,9f);
        // tempGround = new Plane(Vector3.up,Vector3.down);
        tempGround = new Plane(Vector3.up, Vector3.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        this.transform.position = eventData.position;
        // Vector2 mousepos = new Vector2();
        Vector3 point = new Vector3();
        // mousepos.x = currentEvent.mousePosition.x;
        // mousepos.y = currentEvent.mousePosition.y;

        Vector3 mousepos = new Vector3();
        mousepos.x = Mouse.current.position.x.ReadValue();
        mousepos.y = Mouse.current.position.y.ReadValue();
        // point = cam.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, cam.nearClipPlane));
        mousepos.z = 9f;
        // point = cam.ScreenToWorldPoint(new Vector3(mousepos.x, 0.5f, mousepos.y));
        point = cam.ScreenToWorldPoint(mousepos);
        Ray tempRay = cam.ScreenPointToRay(mousepos);
        float enter = 0.0f;
        // tempGround.Raycast(tempRay, out enter);
        if(tempGround.Raycast(tempRay, out enter))
        {
            Vector3 hitPoint = tempRay.GetPoint(enter);
            Debug.Log(" hit point is " + hitPoint);
            tempTestObj.transform.position = hitPoint;
        }

        Debug.Log("mouse pos is " + mousepos
            + " point is " + point
            + " origin of ray " + tempRay.origin
            + " direction of ray " + tempRay.direction);

        // tempTestObj.transform.position = point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("[OnEndDrag]");
    }

    [TestMethod]
    public void DisplayPos()
    {
        Debug.Log("[DisplayPos], " + tempTestObj.transform.position);
    }

    

}
