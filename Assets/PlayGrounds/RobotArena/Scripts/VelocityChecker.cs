using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityChecker : MonoBehaviour
{
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private Rigidbody rb;

    private bool bIsOnCollisionStayCalled = false;

    private void Start()
    {
        Invoke("LogVelocity", 0.5f);
    }

    [TestMethod(false)]
    public void LogVelocity()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        
        Debug.Log("Current Velocity: " + rb.linearVelocity
            + " object name is " + gameObject.name);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter: " + collision.gameObject.name
            + " relative velocity is " + collision.relativeVelocity
            + " this obj name " + gameObject.name);
        LogVelocity();

    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit: " + collision.gameObject.name);
        bIsOnCollisionStayCalled = false;
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (bIsOnCollisionStayCalled)
    //    {
    //        return;
    //    }
    //    if (rb == null)
    //    {
    //        rb = GetComponent<Rigidbody>();
    //    }
        

    //    bIsOnCollisionStayCalled = true;
    //    Debug.Log("OnCollisionStay: " + collision.gameObject.name);
    //    Debug.Log("Velocity before collision: " + rb.linearVelocity);
        
    //}

    //public void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("OnTriggerEnter: " + other.name);
    //    LogVelocity();
    //}
    //public void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("OnTriggerExit: " + other.name);
    //}

}
