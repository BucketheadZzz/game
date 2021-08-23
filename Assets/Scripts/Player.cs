using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour, IDamagable, IPlayer
    {
        [SerializeField]
        private PlayerMovement playerMovement;
        private BaseWeapon currentWeapon;
        private bool isInLootZoone;
        private LootZone currentLootZone;
        private WeaponInventory weaponInventory;
        private int hp = 100;

        private void Awake()
        {
            var characterComponent = GetComponent<CharacterController>();
            playerMovement = new PlayerMovement(transform, characterComponent);
            weaponInventory = GetComponentInChildren<WeaponInventory>();
            currentWeapon = GetComponentInChildren<BaseWeapon>();
        }

        private void Update()
        {
            playerMovement?.Move();

            if (Input.GetButtonDown("Fire1"))
            {
                currentWeapon.Shoot();
            }

            if (Input.GetButtonDown("Interact") && isInLootZoone)
            {
                Loot();
            }
        }

        private void Loot()
        {
            if (currentLootZone != null)
            {
                currentWeapon = weaponInventory.ChangeWeapon(currentLootZone.Loot).GetComponent<BaseWeapon>();
                currentWeapon.transform.rotation = transform.rotation;

                Destroy(currentLootZone.gameObject);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            var loozZone = collider.GetComponent<LootZone>();
            if (loozZone != null)
            {
                isInLootZoone = true;
                currentLootZone = loozZone;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            var loozZone = collider.GetComponent<LootZone>();
            if (loozZone != null)
            {
                isInLootZoone = false;
                currentLootZone = null;
            }
        }

        public void Hit(int hpDamage)
        {
            hp -= hpDamage;

            if(hp > 0) return;

            Application.Quit(0);
        }
    }
}
