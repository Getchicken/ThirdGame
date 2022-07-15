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

        public int healAmount;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
        }


        private void OnTriggerEnter(Collider collision)
        {
            // enemy gets damaged after collision (with sword)

            if(collision.tag == "Enemy" && wc.IsAttacking == true)
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                    enemyStats.CallHealing();
                }
            }
        }
    }
}