using System;
using Assets.Scripts.Common;
using Assets.Scripts.Enemies;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Level
{
    [Serializable]
    public class KeyGenerator
    {
        private int keysCount = 3;
        private int enemiesCount;

        [SerializeField]
        private GameObject key;

        public void Start()
        {
            EnemyBroadcaster.Instance.CharacterKilled += OnEnemyKilled;
            EnemyBroadcaster.Instance.EnemiesSpawned += OnEnemiesSpawned;
            KeysBroadcaster.Instance.BroadcastEvent(Events.KeyNumberGenerated, keysCount);
        }

        private void OnEnemyKilled(object info)
        {
            var enemyPosition = (Transform) info;
            if (enemiesCount != keysCount)
            {
                var shouldSpawnKey = Random.Range(0, 1) == 1;
                if (shouldSpawnKey)
                {
                    Object.Instantiate(key, enemyPosition.transform);
                    keysCount--;
                }
            }
            else
            {
                Object.Instantiate(key, enemyPosition.transform);
                keysCount--;
            }

            enemiesCount--;
        }


        private void OnEnemiesSpawned(object info)
        {
            enemiesCount = (int) info;
        }
    }
}
