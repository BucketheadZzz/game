using System;
using Assets.Scripts.Enemies;
using Assets.Scripts.Level;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common
{
    [Serializable]
    public class UiObserver : IObserver
    {
        [SerializeField]
        private Text currentPlayerHpText;

        [SerializeField]
        private TMP_Text enemiesRemainText;

        [SerializeField]
        private Text keysToFindText;

        private const string EnemiesRemain = "Enemies killed: {0}/{1}";
        private int enemiesSpawnedCount;
        private int enemiesKilledCount;

        private const string CurrentHpMessage = "Current HP: {0}/{1}";

        private const string KeysToFindMessage = "Keys found: {0}/{1}";
        private int keysToFind;
        private int keysFound;

        public void Start()
        {
            PlayerBroadcaster.Instance.HpChanged += PlayerHpChanged;
            
            EnemyBroadcaster.Instance.CharacterKilled += OnEnemyKilled;
            EnemyBroadcaster.Instance.EnemiesSpawned += OnEnemySpawned;

            KeysBroadcaster.Instance.KeyCountGenerated += OnKeyCountGenerated;
            KeysBroadcaster.Instance.KeyFound += OnKeyFound;
        }

        private void OnKeyCountGenerated(object info)
        {
            keysToFind = (int) info;
            keysToFindText.text = string.Format(KeysToFindMessage, keysFound, keysToFind);
        }

        private void OnKeyFound(object info)
        {
            keysFound++;
            keysToFindText.text = string.Format(KeysToFindMessage, keysFound, keysToFind);
        }

        private void OnEnemyKilled(object obj)
        {
            enemiesKilledCount++;
            enemiesRemainText.text = string.Format(EnemiesRemain, enemiesKilledCount, enemiesSpawnedCount);
        }

        private void OnEnemySpawned(object info)
        {
            enemiesSpawnedCount = (int)info;
            enemiesRemainText.text = string.Format(EnemiesRemain, enemiesKilledCount, enemiesSpawnedCount);
        }

        private void PlayerHpChanged(object info)
        {
            var playerInfo = (PlayerInfo)info;
            currentPlayerHpText.text = string.Format(CurrentHpMessage, playerInfo.CurrentHp, playerInfo.Max);
        }
    }
}
