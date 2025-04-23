using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityChecker : MonoBehaviour
{
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private Rigidbody rb;

    private bool bIsOnCollisionStayCalled = false;
    [SerializeField] private float startInvokeDelay = 0.2f;

    private void Start()
    {
        Invoke("LogVelocity", startInvokeDelay);
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

    public void ReflectOnCollision(Collider other)
    {
        
        // rb.angularVelocity
        Vector3 incomingVelocity = new Vector3(1, -1, 0); // 들어오는 속도 벡터
        Vector3 normal = new Vector3(0, 1, 0); // 수직 방향 (위쪽)

        Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);
        
        Vector3.Reflect(rb.linearVelocity, other.transform.forward);
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
