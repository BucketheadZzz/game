using System;
using System.Collections;
using Assets.Helpers;
using Assets.Scripts.Common;
using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    public class Enemy : DamageableObject
    {
        private BaseWeapon weapon;
        private float attackTemp;
        private const float coolDown = 1f;

        private NavMeshAgent navMeshAgent;
        private Transform currentWayPoint;
        private bool isShouting;

        public Tuple<Transform, Transform> PathPositions;

        [SerializeField]
        private Transform headPoint;

        private GameObject discoveredEnemy;
        private float maxObservationRange = 15f;
        private float maxRangeTilEnemy = 16f;
        private float weaponRange = 12f;
        private float pursuingEnemyCooldown = 2f;

        private void Awake()
        {
            weapon = GetComponentInChildren<BaseWeapon>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.avoidancePriority = Random.Range(1, 50);
        }

        private void Update()
        {
            FindEnemy();
            CheckEnemyPosition();
            AttackIfPossible();
        }

        private void FixedUpdate()
        {
            if (discoveredEnemy != null)
            {
                transform.LookAt(discoveredEnemy.transform);
            }

            if (PathPositions != null)
            {
                Patrol();
            }
        }

        private void CheckEnemyPosition()
        {
            if (discoveredEnemy == null) return;

            float dist = Vector3.Distance(transform.position, discoveredEnemy.transform.position);
            if (dist > maxRangeTilEnemy)
            {
                StartCoroutine(ResetObservedEnemy());
            }
        }

        private IEnumerator ResetObservedEnemy()
        {
            yield return new WaitForSeconds(pursuingEnemyCooldown);
            discoveredEnemy = null;
        }

        private void AttackIfPossible()
        {
            if (discoveredEnemy != null && Vector3.Distance(transform.position, discoveredEnemy.transform.position) <= weaponRange)
            {
                if (attackTemp > 0)
                    attackTemp -= Time.deltaTime;

                if (attackTemp < 0)
                    attackTemp = 0;

                if (attackTemp == 0 && weapon != null)
                {
                    weapon.Shoot();
                    attackTemp = coolDown;
                    isShouting = true;
                }
            }
            else
            {
                isShouting = false;
            }
        }

        private void FindEnemy()
        {
            if (discoveredEnemy != null) return;

            var isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward,
                out RaycastHit hit, transform.rotation, maxObservationRange);
            if (isHit && hit.collider.GetPlayerGameObject() != null)
            {
                discoveredEnemy = hit.collider.GetPlayerGameObject();
            }
        }

        private void Patrol()
        {
            if (discoveredEnemy != null && !isShouting)
            {
                var dist = Vector3.Distance(transform.position, discoveredEnemy.transform.position);
                if (dist > weaponRange && dist < maxRangeTilEnemy)
                {
                    navMeshAgent.SetDestination(discoveredEnemy.transform.position);
                    navMeshAgent.isStopped = false;
                }
            }
            else
            {
                if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && !isShouting)
                {
                    navMeshAgent.isStopped = false;
                    var newDestination = currentWayPoint == PathPositions.Item1 ? PathPositions.Item2 : PathPositions.Item1;
                    currentWayPoint = newDestination;
                    navMeshAgent.SetDestination(newDestination.position);
                }
            }

            if (isShouting)
            {
                navMeshAgent.isStopped = true;
            }
        }

        public void SetDestination()
        {
            if (PathPositions != null)
            {
                navMeshAgent.SetDestination(PathPositions.Item1.position);
            }
        }
    }
}
