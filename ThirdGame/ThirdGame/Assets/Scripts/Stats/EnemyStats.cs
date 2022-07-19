using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG 
{
    public class EnemyStats : CharacterStats
    {
        public GameObject Enemy01;
        Animator animator;

        public EnemyAi enemyAi;
        Collider capCollider;

        public float waitTime;

        // Healing the Player on hit
        public GameObject ninjaModel;
        PlayerStats playerStats;
        public int healAmount;
        bool canHeal = true;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            enemyAi = GetComponentInParent<EnemyAi>();
            enemyAi.enabled = true;

            // bug fix for infinite score
            capCollider = GetComponent<Collider>();
        }
    
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 20;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            //animator.Play("DamageToEnemy");
            animator.SetTrigger("Hit01");

            // death anim
            if(currentHealth <= 0) 
            {
                currentHealth = 0;

                // Death anim + bug fix and destroy for death
                animator.SetBool("isDead", true);
                capCollider.enabled = false;
                enemyAi.enabled = false;
                Object.Destroy(Enemy01, waitTime);
            }
        }

        public void CallHealing()
        {
            if(canHeal == true)
            {
                //for healing
                playerStats = ninjaModel.GetComponent<PlayerStats>();
                playerStats.Healing(healAmount);

                canHeal = false;
                StartCoroutine(ResetHealBool());
            }
        }

        IEnumerator ResetHealBool()
        {
            yield return new WaitForSeconds(0.3f);
            canHeal = true;
        }
    }
}
