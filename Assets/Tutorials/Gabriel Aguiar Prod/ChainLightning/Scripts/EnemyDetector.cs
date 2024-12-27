using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GabrielAguiar
{
    public class EnemyDetector : MonoBehaviour
    {
        List<GameObject> enemiesInRange = new List<GameObject>();

        public GameObject GetClosestEnemy()
        {
            if(enemiesInRange.Count <= 0)
            {
                Debug.LogError("EnemiesInRange list is empty");
                return null;
            }
            GameObject bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (GameObject potentialTarget in enemiesInRange)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            return bestTarget;
        }

        public List<GameObject> GetEnemiesInRange()
        {
            return enemiesInRange;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemiesInRange.Add(other.gameObject);
            }
            else if(!enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Add(other.gameObject);
            }
            //else if (other.CompareTag("ChainLightning"))
            //{
            //    enemiesInRange.Add(other.gameObject);
            //}
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemiesInRange.Remove(other.gameObject);
            }
            else if (enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Remove(other.gameObject);
            }
            //else if (other.CompareTag("ChainLightning"))
            //{
            //    enemiesInRange.Remove(other.gameObject);
            //}
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
