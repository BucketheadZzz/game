using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class LootZone : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] lootableItems;

        private bool shouldGenerateLoot;
 
        private float degreesToRotate = 20f;

        public GameObject Loot { get; private set; }

        private static readonly System.Random random = new System.Random();

        void Awake()
        {
            shouldGenerateLoot = random.Next(0, 10000) % 2 == 0;
            if (shouldGenerateLoot)
            {
                var lootableIndex = random.Next(0, lootableItems.Length - 1);
                var itemToGenerate = lootableItems[lootableIndex];
                Loot = Instantiate(itemToGenerate, transform.position, Quaternion.identity);
                Loot.transform.parent = transform;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (Loot != null)
            {
                Loot.transform.Rotate(Vector3.up * Time.deltaTime * degreesToRotate, Space.Self);
            }
        }
    }
}
