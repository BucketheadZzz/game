using Assets.Scripts.Enemies;
using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class GlobalObserver : MonoBehaviour
    {
        [SerializeField]
        private EnemyObserver enemyObserver;

        [SerializeField]
        private UiObserver uiObserver;

        [SerializeField]
        private KeyGenerator keyGenerator;
       
        void Awake()
        {
            uiObserver.Start();
            enemyObserver.Start();
            keyGenerator.Start();
        }
    }
}
