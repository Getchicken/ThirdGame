using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG 
{
    public class PlayerStats : CharacterStats   
    {
        public GameObject PlayerController02;
        public GameObject RespawnButton;

        PlayerMovement01 playerMovement01;
        private float waitTime = 4f;

        [Header("Detection")]
        Animator animator;

        public HealthBar healthbar;

        private void Awake()
        {
            animator = GetComponentInParent<Animator>();
            
            // for the death
            playerMovement01 = GetComponentInParent<PlayerMovement01>();
            playerMovement01.enabled = true;
        }
    
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;
            healthbar.SetCurrentHealth(currentHealth);

            // hurt anim
            animator.SetTrigger("isHurt");

            // death anim
            Die();
        }

        public void Die()
        {
            if(currentHealth <= 0) 
            {
                currentHealth = 0;
                animator.SetTrigger("isDead");

                // player can see cursor to click buttons
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Enable RespawnButton
                RespawnButton.SetActive(true);

                //Player cant move with no health and gets object gets destroyed
                playerMovement01.enabled = false;
                Object.Destroy(PlayerController02, waitTime);
            }
        }
    }
}
