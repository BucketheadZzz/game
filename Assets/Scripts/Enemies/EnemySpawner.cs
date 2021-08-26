using System;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour, IEnemySpawner
    {

        [SerializeField]
        private GameObject[] enemies;

        private System.Random random;

        private const int maxSpawnRange = 10;
        private const int maxDistant = 10;

        public int EnemySpawned { get; private set; }

        private void Start()
        {
            random = new System.Random();
        }

        public void SpawnEnemy()
        {
            var enemyToSpawn = random.Next(0, enemies.Length);
            var positionToSpawnX = random.Next((int)transform.position.x, (int)transform.position.x + maxSpawnRange);
            var positionToSpawnZ = random.Next((int)transform.position.z, (int)transform.position.z + maxSpawnRange);

            var newPosition = new Vector3(positionToSpawnX, transform.position.y, positionToSpawnZ);

            var spawnedEnemy = Instantiate(enemies[enemyToSpawn], newPosition, Quaternion.identity);
        

            var x = random.Next(maxDistant / 2, maxDistant);
            var z = random.Next(maxDistant / 2, maxDistant);
            var startPointPosition =
                new Vector3(transform.position.x + x,
                    transform.position.y,
                    transform.position.z + z);

            var endPointPosition =
                new Vector3(transform.position.x - x,
                    transform.position.y,
                    transform.position.z - z);

            var startPoint = new GameObject("enemyStartPoint");
            startPoint.transform.position = startPointPosition;
            startPoint.transform.parent = spawnedEnemy.transform;

            var endPoint = new GameObject("enemyStopPoint");
            endPoint.transform.position = endPointPosition;
            endPoint.transform.parent = spawnedEnemy.transform;

            var enemyComponent = spawnedEnemy.GetComponent<Enemy>();
            enemyComponent.PathPositions = new Tuple<Transform, Transform>(startPoint.transform, endPoint.transform);

            enemyComponent.SetDestination();
            EnemySpawned++;
        }
    }
}
