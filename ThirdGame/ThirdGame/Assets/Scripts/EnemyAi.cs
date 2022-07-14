using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EG 
{ 
    public class EnemyAi : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask ground, whatIsPlayer;

        // Patroling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //Attacking
        public float timeBetweenAttacks = 2f;
        private bool alreadyAttacked;
        public GameObject projectile;
        public float projectileSpeed;
        public float projectileUp;
        private DoDamage doDamge;

        //States
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        void Awake()
        {
            player = GameObject.Find("PlayerController02").transform;
            agent = GetComponent<NavMeshAgent>();

            //Collider for Attacking
            doDamge = GetComponent<DoDamage>();
            
        }

        private void Update()
        {
            //Check if player is in sight
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if(!playerInSightRange && !playerInAttackRange) 
            {
                Patroling();
            }

            if(playerInSightRange && !playerInAttackRange) 
            {
                ChasePlayer();
            }
            if(playerInAttackRange && playerInSightRange) 
            {
                AttackPlayer();
            }
        }

        private void Patroling()
        {
            // if no walkPoint then search one
            if(walkPointSet == false)
            {
                SearchWalkPoint();
            } 

            //if there is one go there

            if(walkPointSet)
            {
                agent.SetDestination(walkPoint);
            }
            
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // walkPoint reached
            if(distanceToWalkPoint.magnitude < 2f)
            {
                walkPointSet = false;
            }
        }

        private void SearchWalkPoint()
        {
            // calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX,  transform.position.y, transform.position.z + randomZ);
            

            if(Physics.Raycast(walkPoint, -transform.up, 2f, ground))
            {
                walkPointSet = true;
            }
        }

        private void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {
            //FaceTarget();
            //alternitive face target transform.LookAt(player);
            transform.LookAt(player);

            // make enemy stop moving
            agent.SetDestination(transform.position);

            if(!alreadyAttacked)
            {
                // Projectile gets fired with force
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                
                rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
                rb.AddForce(transform.up * projectileUp, ForceMode.Impulse);

                // Attack function to do damage
                

                //Reset Attack + Cooldown
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}