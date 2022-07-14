using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EG 
{
    public class DamageCollider : MonoBehaviour
    {
        public WeaponController01 wc;
        private Collider damageCollider;
        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            //damageCollider.enabled = false;
        }

        /*public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }
        */

        private void OnTriggerEnter(Collider collision)
        {
            // player gets damaged after collision from anything/anyone ____ This part only on enemys to damage players

            if(collision.tag == "Player" && wc.IsAttacking == false)
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if(playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                }
            }
            

            // enemy gets damaged after collision (with sword)

            if(collision.tag == "Enemy" && wc.IsAttacking == true)
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                }
            }
        }
    }
}