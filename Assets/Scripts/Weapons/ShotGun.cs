using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ShotGun : BaseWeapon
    {
        public override void Shoot()
        {
            foreach (var bulltBulletStartPosition in bulletStartPositions)
            {
                var spawnedBulled = Instantiate(bullet, bulltBulletStartPosition.position, Quaternion.identity);
                spawnedBulled.transform.parent = gameObject.transform;
                spawnedBulled.GetComponent<Rigidbody>().AddForce(bulltBulletStartPosition.forward * bulletSpeed);
            }

            if (shotSound != null)
            {
                shotSound.Play();
            }
        }
    }
}
