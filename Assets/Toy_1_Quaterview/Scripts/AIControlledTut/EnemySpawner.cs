using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
// using UnityEngine.Rendering;

namespace LlamAcademy
{
    public class EnemySpawner : MonoBehaviour
    {
        // part 4
        public Transform Player;
        public Transform Target;
        public int NumberOfEnemiesToSpawn = 5;
        public float SpawnDelay = 1f;
        public List<Enemy> EnemyPrefabs = new List<Enemy>();

        public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin;


        private Dictionary<int, ObjectPool<Enemy>> enemyPools = new Dictionary<int, ObjectPool<Enemy>>();


        // Start is called before the first frame update
        void Start()
        {
            ObjectPool<Enemy> tempNew = new ObjectPool<Enemy>(null);
        }

        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);

            int spawnedEnemies = 0;

            while (spawnedEnemies < NumberOfEnemiesToSpawn)
            {
                if (EnemySpawnMethod == SpawnMethod.RoundRobin)
                {
                    SpawnRoundRobinEnemy(spawnedEnemies);
                }
                else if (EnemySpawnMethod == SpawnMethod.Random)
                {
                    SpawnRandomEnemy();
                }
                spawnedEnemies++;
                yield return wait;
            }

        }
        private void SpawnRoundRobinEnemy(int spawnedEnemies)
        {
            int spawnIndex = spawnedEnemies % EnemyPrefabs.Count;
            DoSpawnEnemy(spawnIndex);


        }
        private void SpawnRandomEnemy()
        {
            DoSpawnEnemy(Random.Range(0, EnemyPrefabs.Count));
        }

        private void DoSpawnEnemy(int spawnIndex)
        {
            Enemy spawnedEnemy = enemyPools[spawnIndex].Get();
            if (spawnedEnemy != null)
            {
                NavMeshTriangulation Triangulation = NavMesh.CalculateTriangulation();
                int vertexIndex = Random.Range(0, Triangulation.vertices.Length);

                NavMeshHit hit;
                if (NavMesh.SamplePosition(Triangulation.vertices[vertexIndex], out hit, 2f, 0))
                {
                    spawnedEnemy.Agent.Warp(hit.position);
                    spawnedEnemy.Movement.target = Target;
                    spawnedEnemy.Agent.enabled = true;
                }

            }
            else
            {
                Debug.LogError("unable to fetch enemy");
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public enum SpawnMethod
        {
            RoundRobin,
            Random,
        }

    }
}
