using System;
using Assets.Scripts.Common;
using Assets.Scripts.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Level
{
    [Serializable]
    public class KeyGenerator : MonoBehaviour
    {
        private int keysCount;
        private int enemiesCount;

        [SerializeField]
        private GameObject key;

        public void Init()
        {
            EnemyBroadcaster.Instance.CharacterKilled += OnEnemyKilled;
            EnemyBroadcaster.Instance.EnemiesSpawned += OnEnemiesSpawned;
        }

        private void OnEnemyKilled(object info)
        {
            var enemyPosition = (Transform) info;
            if (enemiesCount != keysCount)
            {
                var shouldSpawnKey = Random.Range(0, 1) == 1;
                if (shouldSpawnKey)
                {
                    Instantiate(key, enemyPosition.transform.position, Quaternion.identity);
                    keysCount--;
                }
            }
            else
            {
                Instantiate(key, enemyPosition.transform.position, Quaternion.identity);
                keysCount--;
            }

            enemiesCount--;

            if(keysCount == 0)
            {
                EnemyBroadcaster.Instance.CharacterKilled -= OnEnemyKilled;
                EnemyBroadcaster.Instance.EnemiesSpawned -= OnEnemiesSpawned;
            }
        }


        private void OnEnemiesSpawned(object info)
        {
            enemiesCount = (int) info;
            keysCount = enemiesCount > 1 ? enemiesCount / 2 : 1;
            KeysBroadcaster.Instance.BroadcastEvent(Events.KeyNumberGenerated, keysCount);
        }
    }
}
