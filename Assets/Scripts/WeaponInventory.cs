using UnityEngine;

namespace Assets.Scripts
{
    public class WeaponInventory : MonoBehaviour
    {
        [SerializeField] 
        public GameObject defaultWeapon;

        private GameObject currentWeapon;

        private void Awake()
        {
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
    }
}
