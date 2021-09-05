using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BaseWeapon : MonoBehaviour, ILootable
    {
        [SerializeField]
        protected GameObject bullet;

        [SerializeField]
        protected Transform[] bulletStartPositions;

        [SerializeField]
        protected float bulletSpeed = 60f;

        [SerializeField]
        public int DamagePerBullet = 20;

        public float WeaponRange = 60f;

        protected AudioSource shotSound;

        private void Awake()
        {
            shotSound = GetComponent<AudioSource>();
        }

        public virtual void Shoot()
        {
            var spawnedBulled = Instantiate(bullet, bulletStartPositions.First().position, Quaternion.identity);
            spawnedBulled.transform.parent = gameObject.transform;
            spawnedBulled.GetComponent<Rigidbody>().AddForce(bulletStartPositions.First().forward * bulletSpeed);

            if (shotSound != null)
            {
                shotSound.Play();
            }
        }
    }
}
