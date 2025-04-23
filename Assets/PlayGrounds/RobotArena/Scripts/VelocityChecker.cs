using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityChecker : MonoBehaviour
{
    SphereCollider sphereCollider;
    Rigidbody rb;
    [TestMethod(false)]
    public void LogVelocity()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        
        Debug.Log("Current Velocity: " + rb.linearVelocity);

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);
        LogVelocity();
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit: " + other.name);
    }

}
