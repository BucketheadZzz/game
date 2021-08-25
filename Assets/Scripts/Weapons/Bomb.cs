using Assets.Helpers;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField]
        private int damage = 1000;

        void OnCollisionEnter(Collision collision)
        {
            var player = collision.GetPlayerGameObject();
            if (player != null)
            {
                player.GetComponent<IDamageable>()?.Hit(damage);
            }
        }
    }
}
