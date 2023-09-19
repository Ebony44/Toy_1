using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Tut_EnemyAI : MonoBehaviour
{

    private Tut_EnemyPathfindingMovement pathfindingMovement;

    private Vector3 startingPoisition;
    private Vector3 roamPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPoisition = transform.position;
        pathfindingMovement = GetComponent<Tut_EnemyPathfindingMovement>();

    }

    private Vector3 GetRoamingPosition()
    {
        return startingPoisition + GetRandomDir() * Random.Range(1f, 7f);
    }
    public Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f,  UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        pathfindingMovement.SetTarget(roamPosition, true);

        // pathfindingMovement.MoveToRandomPos(transform.position, 20f, roamPosition);
        float reachedPositionDistnace = 1f;
        if(Vector3.Distance(transform.position,roamPosition) <  reachedPositionDistnace)
        {
            roamPosition = GetRoamingPosition();
            // Debug.LogError("distance is " + Vector3.Distance(transform.position, roamPosition));
        }
        else
        {
            // Debug.LogError("distance is " + Vector3.Distance(transform.position, roamPosition));
        }
        // pathfindingMovement.SetTarget(transform, true);
    }
}
