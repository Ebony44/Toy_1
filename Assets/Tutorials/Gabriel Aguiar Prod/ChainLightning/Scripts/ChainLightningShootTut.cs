using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace GabrielAguiar
{
    public class ChainLightningShootTut : MonoBehaviour
    {
        [SerializeField] float refreshRate = 0.01f;
        [SerializeField] Transform shootPoint;
        [SerializeField] EnemyDetector enemyDetector;
        [SerializeField] GameObject linerendererPrefab;
        
        bool bIsShooting = false;
        bool bShot = false;
        List<GameObject> spawnedLineRenderer = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Mouse.current.leftButton.isPressed)
            {
                if(enemyDetector.GetEnemiesInRange().Count > 0 && !bIsShooting)
                {
                    // bIsShooting = true;
                    // StartCoroutine(ChainLightning());
                    StartShooting();
                }
            }
            if(Mouse.current.leftButton.wasReleasedThisFrame)
            {
                StopShooting();
            }
        }

        private void StartShooting()
        {
            bIsShooting = true;
            if(enemyDetector != null&& shootPoint != null && linerendererPrefab != null)
            {
                if(!bShot)
                {
                    CreateNewLineRenderer(shootPoint, enemyDetector.GetClosestEnemy().transform);
                    //GameObject closestEnemy = enemyDetector.GetClosestEnemy();
                    //if (closestEnemy != null)
                    //{
                    //    GameObject linerenderer = Instantiate(linerendererPrefab, shootPoint.position, Quaternion.identity);
                    //    linerenderer.GetComponent<LineRenderer>().SetPosition(0, shootPoint.position);
                    //    linerenderer.GetComponent<LineRenderer>().SetPosition(1, closestEnemy.transform.position);
                    //}
                    bShot = true;
                }
            }
        }

        private void CreateNewLineRenderer(Transform startPos, Transform endPos)
        {
            GameObject lineR = Instantiate(linerendererPrefab, startPos.position, Quaternion.identity);
            spawnedLineRenderer.Add(lineR);
            StartCoroutine(UpdateLineRenderer(lineR, startPos, endPos));
        }

        IEnumerator UpdateLineRenderer(GameObject lineRenderer, Transform startPos, Transform endPos)
        {
            if(bIsShooting && bShot && lineRenderer != null)
            {
                lineRenderer.GetComponent<LineRendererController>().SetPosition(startPos, endPos);
                yield return new WaitForSeconds(refreshRate);
            }
            //while(true)
            //{
            //    lineRenderer.SetPosition(0, startPos.position);
            //    lineRenderer.SetPosition(1, endPos.position);
            //    yield return null;
            //}
        }
        private void StopShooting()
        {
            bIsShooting = false;
            bShot = false;
        }

    }

    

}
