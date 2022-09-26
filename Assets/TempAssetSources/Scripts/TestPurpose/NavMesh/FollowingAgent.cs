using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingAgent : MonoBehaviour
{
    public Transform target;

    public NavMeshAgent self;

    public bool bCanFollow = false;

    // Update is called once per frame
    void Update()
    {
        if(bCanFollow)
        {
            self.SetDestination(target.position);
        }
    }


}
