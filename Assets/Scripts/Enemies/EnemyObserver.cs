using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyObserver : MonoBehaviour
    {
        private IList<IEnemySpawner> enemySpawners;

        [SerializeField]
        private int maxEnemyToSpawn = 10;

        private int currentKilledEnemy;

        private System.Random random;

        private void Awake()
        {
            EnemyKillBroadcaster.GetCurrentInstance().EnemyKilled += OnEnemyKilled;
        }

        private void Start()
        {
            enemySpawners = FindObjectsOfType<MonoBehaviour>().OfType<IEnemySpawner>().ToList();
            random = new System.Random();
        }
        private void Update()
        {
            if (enemySpawners != null)
            {
                var enemyCount = enemySpawners.Sum(x => x.EnemySpawned);

                if (currentKilledEnemy == maxEnemyToSpawn)
                {
                    return;
                }

                if (enemyCount  == maxEnemyToSpawn)
                {
                    return;
                }

                var spawnerIndex = random.Next(0, enemySpawners.Count);
                var spawner = enemySpawners[spawnerIndex];

                spawner.SpawnEnemy();
            }
        }

        private void OnEnemyKilled()
        {
            currentKilledEnemy++;
        }
    }
}
