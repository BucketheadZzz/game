using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        private bool isColliding;

        private float bulletLifeTime = 3f;

        void OnCollisionEnter(Collision collision)
        {
            if (isColliding) return;
            var damagable = collision.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
            if (damagable != null)
            {
                isColliding = true;
                var weaponDamage = gameObject.GetComponentInParent<BaseWeapon>();
                damagable.Hit(weaponDamage.DamagePerBullet);
            }

            Destroy(gameObject, bulletLifeTime);
        }

        void Update()
        {
            isColliding = false;
        }
    }
}
