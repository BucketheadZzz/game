using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour, IEnemySpawner
    {

        [SerializeField]
        private GameObject[] enemies;

        private const int maxSpawnRange = 10;
        private const int maxDistant = 10;

        public int EnemySpawned { get; private set; }

        public void SpawnEnemy()
        {
            var enemyToSpawn = Random.Range(0, enemies.Length);
            var positionToSpawnX = Random.Range((int)transform.position.x, (int)transform.position.x + maxSpawnRange);
            var positionToSpawnZ = Random.Range((int)transform.position.z, (int)transform.position.z + maxSpawnRange);

            var newPosition = new Vector3(positionToSpawnX, transform.position.y, positionToSpawnZ);

            var spawnedEnemy = Instantiate(enemies[enemyToSpawn], newPosition, Quaternion.identity);

            var x = Random.Range(maxDistant / 2, maxDistant);
            var z = Random.Range(maxDistant / 2, maxDistant);
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

            var endPoint = new GameObject("enemyStopPoint");
            endPoint.transform.position = endPointPosition;

            var enemyComponent = spawnedEnemy.GetComponent<Enemy>();
            enemyComponent.PathPositions = new Tuple<Transform, Transform>(startPoint.transform, endPoint.transform);

            enemyComponent.SetDestination();
            EnemySpawned++;
        }
    }
}
