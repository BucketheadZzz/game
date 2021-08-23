using System;
using Assets.Helpers;
using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies
{
    public class Enemy : DamagableObject
    {
        private BaseWeapon weapon;
        private bool isEnemyInRange;
        private float attackTemp;
        private const float coolDown = 1f;
        private Transform enemyPosition;

        private NavMeshAgent navMeshAgent;
        private Transform currentWayPoint;
        public Tuple<Transform, Transform> PathPositions;

        private void Awake()
        {
            weapon = GetComponentInChildren<BaseWeapon>();
            navMeshAgent = GetComponent<NavMeshAgent>();
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

                if (attackTemp == 0)
                {
                    if (weapon != null)
                    {
                        weapon.Shoot();
                        attackTemp = coolDown;
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (enemyPosition != null)
            {
                transform.LookAt(enemyPosition);
            }

            if (PathPositions != null)
            {
                Patrol();
            }
        }

        private void Patrol()
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                var newDestination = currentWayPoint == PathPositions.Item1 ? PathPositions.Item2 : PathPositions.Item1;
                navMeshAgent.SetDestination(newDestination.position);
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
