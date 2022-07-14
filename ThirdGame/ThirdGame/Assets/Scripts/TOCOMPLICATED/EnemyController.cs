using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EG 
{
    public class EnemyController : MonoBehaviour
    {
        public float lookRadius = 10f;

        Transform target;
        NavMeshAgent agent;

        private void Start()
        {
            target = PlayerManager.instance.player.transform;
            agent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if(distance <= lookRadius)
            {
                agent.SetDestination(target.position);

                if(distance <= agent.stoppingDistance)
                {
                    //attack
                    
                    //face
                    FaceTarget();
                }
            }
        }

        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation, Time.deltaTime * 5f);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }
   
    }
}
