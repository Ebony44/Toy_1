using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDeployChecker : MonoBehaviour
{
    public Collider checkerCollider;
    public Action<bool> onCollided;

    public void CheckUnitDeploy(bool bIsDeployable)
    {
        onCollided(bIsDeployable);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("[OnCollisionEnter], player can NOT deploy units");
        // CheckUnitDeploy(false);
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("[OnCollisionExit], player can deploy units");
        // CheckUnitDeploy(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("[OnTriggerEnter], player can NOT deploy units");
        CheckUnitDeploy(false);
    }
    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("[OnTriggerExit], player can deploy units");
        CheckUnitDeploy(true);
    }

}
