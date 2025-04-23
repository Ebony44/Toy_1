using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityChecker : MonoBehaviour
{
    SphereCollider sphereCollider;
    Rigidbody rb;
    public void LogVelocity()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        

        // Debug.Log("Current Velocity: " + rb.velocity);
    }
}
