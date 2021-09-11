using System.Linq;
using Assets.Helpers;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bomb : BaseWeapon
    {
        public bool IsSet;

        private Rigidbody bombRigidbody;
        private float bombThrowForce = 30f;

        private void Awake()
        {
            bombRigidbody = GetComponent<Rigidbody>();
            if (!IsSet)
            {
                bombRigidbody.isKinematic = true;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (IsSet)
            {
                var player = collider.GetPlayerGameObject();
                if (player != null)
                {
                    var playerRigidBody = player.GetComponent<Rigidbody>();
                    playerRigidBody.AddForce((Vector3.up + Vector3.back) * bombThrowForce, ForceMode.Impulse);
                    player.GetComponent<IDamageable>()?.Hit(DamagePerBullet);
                }
            }
        }


        public override void Shoot()
        {
            var spawnedBomb = Instantiate(bullet, bulletStartPositions.First().position, Quaternion.identity);
            var bombScript = spawnedBomb.GetComponent<Bomb>();
            var bombRigidBody = spawnedBomb.GetComponent<Rigidbody>();
            bombRigidBody.isKinematic = false;
            bombRigidBody.AddForce(Vector3.forward * bombThrowForce);
            bombScript.IsSet = true;
        }
    }
}
