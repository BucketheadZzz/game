using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Inventory
    {
        private readonly IList<GameObject> items = new List<GameObject>();
        private int maxItems = 10;

        public IEnumerable<GameObject> Items => items.AsEnumerable();

        public void AddItem(GameObject item)
        {
            if (items.Count < maxItems)
            {
                items.Add(item);
            }
        }

        public void RemoveItem(GameObject item)
        {
            items.Remove(item);
        }

        public void ClearInventory()
        {
            items.Clear();
        }
    }
}
