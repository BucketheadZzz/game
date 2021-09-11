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
        private KeyObserver keyObserver;
       
        void Awake()
        {
            uiObserver.Start();
            keyObserver.Start();
            enemyObserver.Start();
        }
    }
}
