using Assets.Scripts;
using UnityEngine;

namespace Assets.Helpers
{
    public static class CollisionsExtensions
    {
        public static GameObject GetPlayerGameObject(this Collision collision)
        {
            return collision.gameObject.GetComponent(typeof(IPlayer))?.gameObject;
        }

        public static GameObject GetPlayerGameObject(this Collider collider)
        {
            return collider.gameObject.GetComponent(typeof(IPlayer))?.gameObject;
        }
    }
}
