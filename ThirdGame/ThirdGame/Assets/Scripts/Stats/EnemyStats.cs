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

        public float waitTime;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            enemyAi = GetComponentInParent<EnemyAi>();
            enemyAi.enabled = true;
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
                Debug.Log("Enemy Slayed");
            
                animator.SetBool("isDead", true);
                enemyAi.enabled = false;
                Object.Destroy(Enemy01, waitTime);
            
            }
        

        }
    }
}
