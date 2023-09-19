using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tut_EnemyPathfindingMovement : MonoBehaviour
{
    private NavMeshAgent enemy;

    // public Transform targetTrans;
    public Vector3 targetPosition;
    public bool bIsMovable;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Vector3 target, bool bIsMovable)
    {
        // targetTrans = target;
        targetPosition = target;
        this.bIsMovable = bIsMovable;
        if(this.bIsMovable == true)
        {
            MoveToPos(targetPosition);
        }
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        if (this.bIsMovable == true)
        {
            MoveToPos(targetPosition);
        }
    }
    public void SetCanMove(bool bIsMovable)
    {
        this.bIsMovable = bIsMovable;
    }
    public void MoveToPos(Vector3 targetPos)
    {
        Debug.Log("[MoveToPos], move to pos " + targetPos);

        NavMeshHit hit;
        
        if(NavMesh.SamplePosition(targetPos, out hit, 1.0f, NavMesh.AllAreas))
        // if(NavMesh.FindClosestEdge())
        {
            targetPos = hit.position;
            Debug.Log("SamplePosition true, hit position is " + hit.position
                + " origin pos is " + targetPos);
        }
        else
        {
            NavMesh.SamplePosition(targetPos, out hit, 1.0f, NavMesh.AllAreas);
            Debug.Log("SamplePosition false, hit position is " + hit.position
                + " origin pos is " + targetPos);
        }
        

        enemy.SetDestination(targetPos);
    }
    public void MoveToRandomPos(Vector3 currentPos, float range, Vector3 targetPoint)
    {
        RandomPoint(currentPos, range, out targetPoint);
        // NavMesh.SamplePosition(targetpo)
        enemy.SetDestination(targetPoint);
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void ConvertValidPoint()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // enemy.SetDestination(playerTarget.position);
    }
}
