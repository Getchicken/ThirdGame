using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG 
{
    public class DoDamage : MonoBehaviour
    {
        
        Collider doDamageCollider;
        public int currentWeaponDamage = 25;
        EnemyAi enemyAi;
        //public GameObject NinjaModel;
        //Collider playerCollider;

        

        private void Awake()
        {
            enemyAi = GetComponent<EnemyAi>();
            doDamageCollider = GetComponent<Collider>();
            doDamageCollider.gameObject.SetActive(true);
            doDamageCollider.isTrigger = true;

            //playerCollider = NinjaModel.GetComponent<Collider>();
        }


        private void OnTriggerEnter(Collider collision)
        {
            // player gets damaged after collision from anything/anyone ____ This part only on enemys to damage players
            
            if(collision.tag == "Player" ) 
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if(playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                }
            }
        }

        /*
        private void OnTriggerEnter(Collider playerCollider)
        {
            // player gets damaged after collision from anything/anyone ____ This part only on enemys to damage players
            
            if(NinjaModel.tag == "Player" && enemyAi.skelAttacking) 
            {
                PlayerStats playerStats = NinjaModel.GetComponent<PlayerStats>();

                if(playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                    Debug.Log("damage taken");
                }
            }
        }
        */
    }
}









