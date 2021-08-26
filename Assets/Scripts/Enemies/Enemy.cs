using System;
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
        private bool isEnemyInRange;
        private float attackTemp;
        private const float coolDown = 1f;
        private Transform enemyPosition;

        private NavMeshAgent navMeshAgent;
        private Transform currentWayPoint;
        private bool isShouting;

        public Tuple<Transform, Transform> PathPositions;

        private void Awake()
        {
            weapon = GetComponentInChildren<BaseWeapon>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.avoidancePriority = Random.Range(1, 50);
        }

        private void Update()
        {
            AttackIfPossible();
        }

        private void AttackIfPossible()
        {
            if (isEnemyInRange)
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

        private void FixedUpdate()
        {
            if (enemyPosition != null)
            {
                transform.LookAt(enemyPosition);
            }

            if (PathPositions != null && !isShouting)
            {
                Patrol();
            }
        }

        private void Patrol()
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && !isShouting)
            {
                navMeshAgent.isStopped = false;
                var newDestination = currentWayPoint == PathPositions.Item1 ? PathPositions.Item2 : PathPositions.Item1;
                currentWayPoint = newDestination;
                navMeshAgent.SetDestination(newDestination.position);
            }

            if (isShouting)
            {
                navMeshAgent.isStopped = true;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            var player = collider.GetPlayerGameObject();
            if (player != null)
            {
                enemyPosition = player.transform;
                isEnemyInRange = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            var player = collider.GetPlayerGameObject();
            if (player != null)
            {
                isEnemyInRange = false;
                enemyPosition = null;
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
