using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public abstract class DamageableObject: MonoBehaviour, IDamageable
    {
        [SerializeField] private int hp = 100;

        public bool IsDead { get; set; }

        public virtual void Hit(int hpDamage)
        {
            if (!IsDead)
            {
                hp -= hpDamage;
                if (hp > 0) return;
                EnemyBroadcaster.Instance.BroadcastEvent(Events.CharacterKilled, transform);
                IsDead = true;
                Destroy(gameObject);
            }
        }
    }
}
