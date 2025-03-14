using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LlamAcademy
{

    public class EnemyMovement : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private Animator animator;
        public float updateRate = 0.1f;
        private NavMeshAgent agent;
        public Transform[] waypointPaths;

        private const string isWalking = "isWalking";


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            if(animator != null)
            {
                animator.SetBool(isWalking, (agent.velocity.magnitude > 0.01f));
                //Debug.Log("is walking? " + animator.GetBool(isWalking)
                //    + " agent velocity is? " + (agent.velocity.magnitude > 0.01f));
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(FollowTarget());
        }

        [TestMethod(false)]
        public void TestStartFollow()
        {
            StartCoroutine(FollowTarget());
        }

        private IEnumerator FollowTarget()
        {
            WaitForSeconds wait = new WaitForSeconds(updateRate);
            while (enabled)
            {
                agent.SetDestination(target.transform.position);

                yield return wait;
            }
        }

        private IEnumerator RotateToWayPoints()
        {
            if (waypointPaths.Length == 0)
            {
                Debug.LogError("there are no waypoint paths");
                yield break;
            }
            int currentIndex = 0;
            var currentPath = waypointPaths[currentIndex];
            // TODO
            while (enabled)
            {
                agent.SetDestination(currentPath.position);
                if (Vector3.Distance(agent.transform.position, currentPath.position) <= 1f)
                {
                    currentIndex++;
                    if (currentIndex > waypointPaths.Length)
                    {
                        currentIndex = 0;
                    }
                    yield return new WaitForSeconds(1f);
                    currentPath = waypointPaths[currentIndex];

                }
            }
            yield return null;

        }



    }
}
