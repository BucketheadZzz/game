using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class DamagableObject: MonoBehaviour, IDamagable
    {
        [SerializeField] private int hp = 100;

        public virtual void Hit(int hpDamage)
        {
            hp -= hpDamage;
            if (hp > 0) return;

            EnemyKillBroadcaster.GetCurrentInstance().BroadcastEvent();
            Destroy(gameObject);
        }
    }
}
