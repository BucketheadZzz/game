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

            if (collision.gameObject.GetComponent<DamagableObject>() is IDamagable hitObject)
            {
                isColliding = true;
                var weaponDamage = gameObject.GetComponentInParent<BaseWeapon>();
                hitObject.Hit(weaponDamage.DamagePerBullet);
            }

            Destroy(gameObject, bulletLifeTime);
        }

        void Update()
        {
            isColliding = false;
        }
    }
}
