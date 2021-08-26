﻿using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Level;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IDamageable, IPlayer
    {
        [SerializeField]
        private PlayerMovement playerMovement;

        [SerializeField]
        private PlayerInfo playerInfo;

        private BaseWeapon currentWeapon;
        private bool isInLootZoone;
        private LootZone currentLootZone;
        private WeaponInventory weaponInventory;
        private Inventory inventory;

        public int ItemsCount => inventory.Items.Count();
        public bool IsDead { get; private set; }

        private void Awake()
        {
            var characterComponent = GetComponent<CharacterController>();
            playerMovement = new PlayerMovement(transform, characterComponent);
            weaponInventory = GetComponentInChildren<WeaponInventory>();
            currentWeapon = GetComponentInChildren<BaseWeapon>();
            inventory = new Inventory();
        }

        private void Start()
        {
            PlayerBroadcaster.Instance.BroadcastEvent(Events.PlayerHpChanged, playerInfo);
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

            var key = collider.GetComponent<Key>();
            if (key != null)
            {
                var keyObject = collider.gameObject;
                inventory.AddItem(keyObject);
                Destroy(keyObject);

                KeysBroadcaster.Instance.BroadcastEvent(Events.KeyFound);
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
            playerInfo.CurrentHp -= hpDamage;

            PlayerBroadcaster.Instance.BroadcastEvent(Events.PlayerHpChanged, playerInfo);
            if (playerInfo.CurrentHp > 0) return;

            IsDead = true;

            Destroy(gameObject);
        }
    }
}