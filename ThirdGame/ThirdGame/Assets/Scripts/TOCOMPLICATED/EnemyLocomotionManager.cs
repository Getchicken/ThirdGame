using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        NavMeshAgent navmeshAgent;

        Rigidbody enemyRb;
        public CharacterStats currentTarget;


        public LayerMask detectionLayer;
        public float distanceFromTarget;
        public float stoppingDistance = 1f;
        public float rotationSpeed = 15f;


        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRb = GetComponent<Rigidbody>();
        }
        
        
        void Start()
        {
            navmeshAgent.enabled = false;
            enemyRb.isKinematic = false;
        }
        public void HandelDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if(characterStats != null)
                {
                    //check for team id

                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if(viewableAngle > enemyManager.minimumDitectionAngle && viewableAngle < enemyManager.maximumDitectionAngle)
                    {
                        currentTarget = characterStats;
                    }
                }
            }
        }

        private void HandleMoveToTarget()
        {
            Vector3 targetDirection = currentTarget.transform.position - transform.position;

            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            //if we are performing an action, stop our movement.
            if(enemyManager.isPerformingAction)
            {
                //enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navmeshAgent.enabled = false;
            }
            else
            {
                if(distanceFromTarget > stoppingDistance)
                {
                    //enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                }
                else if(distanceFromTarget <= stoppingDistance)
                {
                    //enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }

            HandleRotateTowardsTarget();

            navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;
        } 

        private void HandleRotateTowardsTarget()
        {
            //rotate manually

            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if(direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
            }
            // rotate with pathfinding (navmesh)
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyRb.velocity;

                navmeshAgent.enabled = true;
                navmeshAgent.SetDestination(currentTarget.transform.position);
                enemyRb.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navmeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
            }

            navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;
        }
    }
}