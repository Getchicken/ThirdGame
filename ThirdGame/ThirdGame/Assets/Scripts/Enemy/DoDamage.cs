using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG 
{
    public class DoDamage : MonoBehaviour
    {
        
        private Collider doDamageCollider;
        public int currentWeaponDamage = 25;

        private void Awake()
        {
            doDamageCollider = GetComponent<Collider>();
            doDamageCollider.gameObject.SetActive(true);
            doDamageCollider.isTrigger = true;
        }


        private void OnTriggerEnter(Collider collision)
        {
            // player gets damaged after collision from anything/anyone ____ This part only on enemys to damage players
            
            if(collision.tag == "Player")
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if(playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                }
            }
        }
    }
}









