using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GreyWolf
{
    public class SpiderController : GlobalEnemyController
    {
        [Header("Settings")]
        [SerializeField][Range(1, 5)] float movementSpeed = 2f;
        [SerializeField][Range(3, 10)] float playerDistance = 5f;
        [SerializeField][Range(3, 7)] float viewRadius = 5f;
        [SerializeField][Tooltip("Min and Max NavMesh point offset that Spider can set")][Range(8, 15)] float AI_Offset = 10f;

        NavMeshAgent enemy;
        Vector3 attackPosition;
        bool isTargetSet, isPatrolling = false;
        float onMeshThreshold = 1f;
        float AI_minOffset = 4f;
        float arrivedDestinationOffset = .1f;

        [Header("Debug")]
        [SerializeField] bool showDirectionPoint;

        private void Start()
        {
            enemy = GetComponent<NavMeshAgent>();
            enemy.speed = movementSpeed;
        }
        private void Update()
        {
            Patrolling();
        }

        private void Patrolling()
        {
            // TODO Add movement patrolling

            // ? Do not use GameObject (Maybe). Research how to make patrolling
            if (!isPatrolling)
            {
                Vector3 point = RandomDestinationPoint();

                if (!IsAgentOnNavMesh(point)) return;


                // if (!isObstacleBetween(directionPoint, viewRadius)) return;

                // enemy.SetDestination(directionPoint);

                isPatrolling = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                isPatrolling = false;
            }

        }

        // private bool ArrivedAtDestination()
        // {
        //     if (
        //         transform.position.x >= directionPoint.transform.position.x + arrivedDestinationOffset ||
        //         transform.position.x <= directionPoint.transform.position.x + arrivedDestinationOffset
        //         &&
        //         transform.position.z >= directionPoint.transform.position.z + arrivedDestinationOffset ||
        //         transform.position.z <= directionPoint.transform.position.z + arrivedDestinationOffset
        //     )
        //     {
        //         return true;
        //     }

        //     return false;
        // }
        private Vector3 RandomDestinationPoint()
        {
            float X, Z;

            X = transform.position.x + Random.Range(-AI_Offset, AI_Offset);
            Z = transform.position.z + Random.Range(-AI_Offset, AI_Offset);

            if (X >= 0 && X <= AI_minOffset)
            {
                X = AI_minOffset;
            }
            else if (X <= 0 && X >= -AI_minOffset)
            {
                X = -AI_minOffset;
            }

            if (Z >= 0 && Z <= AI_minOffset)
            {
                Z = AI_minOffset;
            }
            else if (Z <= 0 && Z >= -AI_minOffset)
            {
                Z = -AI_minOffset;
            }

            return new Vector3(X, onMeshThreshold, Z);
        }

        private void AttackPlayerInRange()
        {
            if (Vector3.Distance(transform.position, _service.playerController.GetPosition()) < playerDistance)
            {
                enemy.ResetPath();
                attackPosition = _service.playerController.GetPosition();
                isTargetSet = true;
            }
        }
        public bool IsAgentOnNavMesh(Vector3 targetDestination)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetDestination, out hit, onMeshThreshold, NavMesh.AllAreas)) return true;

            return false;
        }

        // private void OnDrawGizmos()
        // {
        //     if (!showDirectionPoint) return;

        //     Gizmos.color = Color.red;
        //     Gizmos.DrawSphere(directionPoint.transform.position, onMeshThreshold);
        // }

    }
}
