using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    [Serializable]
    public class EnemyObserver : IObserver
    {
        private IList<IEnemySpawner> enemySpawners;

        [SerializeField]
        private int maxEnemyToSpawn = 10;

        private int currentlySpawned;

        public void Start()
        {
            enemySpawners = Resources.FindObjectsOfTypeAll(typeof(EnemySpawner)).Select(x => x as IEnemySpawner).ToList();
            Spawn();
        }

        private void Spawn()
        {
            if (enemySpawners != null && enemySpawners.Count > 0)
            {
                while (currentlySpawned != maxEnemyToSpawn)
                {
                    var spawnerIndex = Random.Range(0, enemySpawners.Count);
                    var spawner = enemySpawners[spawnerIndex];

                    spawner.SpawnEnemy();
                    currentlySpawned++;
                }

                EnemyBroadcaster.Instance.BroadcastEvent(Events.EnemiesSpawned, currentlySpawned);
            }
        }
    }
}
