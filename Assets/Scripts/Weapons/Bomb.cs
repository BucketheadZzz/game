using Assets.Helpers;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField]
        private int damage = 1000;

        private void OnTriggerEnter(Collider collider)
        {
            var player = collider.GetPlayerGameObject();
            if (player != null)
            {
                player.GetComponent<IDamageable>()?.Hit(damage);
            }
        }
    }
}
