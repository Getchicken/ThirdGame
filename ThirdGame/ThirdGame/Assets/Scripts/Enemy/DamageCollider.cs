using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EG 
{
    public class DamageCollider : MonoBehaviour
    {
        public WeaponController01 wc; // muss geändert werden weaponcontroller
        private Collider damageCollider;
        public int currentWeaponDamage = 25;

        // for the score
        public GameObject scoreText;
        public GameObject xpBar;

        // Healing the Player on hit
        public GameObject ninjaModel;
        PlayerStats playerStats;
        public int healAmount = 10;
        public int xpGained = 10;
        
        
        bool canHeal = true;



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
                PlayerStats playerStats = GetComponent<PlayerStats>();
                Score score = scoreText.GetComponent<Score>();
                LevelSystem levelSystem = xpBar.GetComponent<LevelSystem>();

                if (enemyStats != null && enemyStats.currentHealth >= 1)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                    enemyStats.SetHealthbarUi();

                    if(canHeal == true)
                    {
                        //for healing
                        playerStats = ninjaModel.GetComponent<PlayerStats>();
                        playerStats.Healing(healAmount);

                        canHeal = false;
                        StartCoroutine(ResetHealBool());
                    }

                    if(enemyStats.currentHealth <= 0 && enemyStats != null)
                    {
                        //increase score by 2 transfer
                        score.IncreaseScore();
                        levelSystem.GainExperienceFlateRate(xpGained);
                    }
                }
            }
        
        }

        public void IncreaseDamage()
        {
            currentWeaponDamage += 3;
        }

        IEnumerator ResetHealBool()
        {
            yield return new WaitForSeconds(0.5f);
            canHeal = true;
        }
    }
}