using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace VFXLightningTargeting
{
    public class VFXTargetTesting : MonoBehaviour
    {
        // public List<Transform> targetTransform;

        public Transform srcTransform;
        public Transform dstTransform;

        public CircleCollider2D tempCollider;

        public Transform strokePoint;

        public float chainingRadius = 2;


        public void FireLightning()
        {
            // tempCollider.
        }

        [TestMethod(false)]
        public void startFindingRoutine(int iterationCount, float waitTime)
        {
            StartCoroutine(FindingRoutine(iterationCount, waitTime, strokePoint.position));
        }

        public IEnumerator FindingRoutine(int iterationCount, float waitTime, Vector3 startPoint)
        {
            int currentIteration = 0;
            List<Collider> cachedColliders = new List<Collider>(iterationCount + 1);
            
            var tempColliders = GetAllColliderWithinArea(startPoint, chainingRadius);
            
            // HelperFunctions.
            while (iterationCount >= currentIteration)
            {
                yield return new WaitForSeconds(waitTime);
                
                var selectedIndex = Random.Range(0, tempColliders.Count - 1);
                var selectedCollider = tempColliders[selectedIndex];

                Debug.Log("[FindingRoutine], iteration is " + currentIteration
                    + " selected object name is " + tempColliders[selectedIndex].gameObject.name);
                cachedColliders.Add(selectedCollider);

                yield return new WaitForSeconds(waitTime);

                tempColliders = GetAllColliderWithinArea(selectedCollider.gameObject.transform.position, chainingRadius);
                HelperFunctions.RemoveListFromList<Collider>(cachedColliders, tempColliders);
                if(tempColliders.Count == 0)
                {
                    break;
                }
                currentIteration++;



            }
        }

        List<Collider> GetAllColliderWithinArea(Vector3 center, float radius)
        {
            var hitColliders = Physics.OverlapSphere(center, radius).ToList();
            
            Debug.Log("objects in colliders' count is " + hitColliders.Count);
            foreach (var hitCollider in hitColliders)
            {
                // hitCollider.SendMessage("AddDamage");
            }
            return hitColliders;
        }


    }

}

