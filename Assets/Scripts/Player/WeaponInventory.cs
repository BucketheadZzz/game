using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class WeaponInventory : MonoBehaviour
    {
        [SerializeField] 
        private GameObject[] weapons;

        private GameObject currentWeapon;

        private void Awake()
        {
            var defaultWeapon = weapons.First();
            currentWeapon = Instantiate(defaultWeapon, transform.position, Quaternion.identity);
            currentWeapon.transform.parent = transform;
        }

        public GameObject ChangeWeapon(GameObject newWeapon)
        {
            Destroy(currentWeapon);

            currentWeapon = Instantiate(newWeapon, transform.position, Quaternion.identity);
            currentWeapon.transform.parent = transform;

            return currentWeapon;
        }

        public GameObject ChangeWeapon(int index)
        {
            var newWeapon = weapons[index - 1];
            return ChangeWeapon(newWeapon);
        }
    }
}
