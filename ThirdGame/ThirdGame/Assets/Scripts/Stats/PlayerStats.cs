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

        [Header("Healing")]
        public HealthBar healthbar;
        private int selfHealAmount;
        private bool canHeal = true;
        

        [Header("Stamina")]
        public Stamina stamina;
        KeyCode sprintKey = KeyCode.LeftShift;

        private void Awake()
        {
            animator = GetComponentInParent<Animator>();
            
            // for the death
            playerMovement01 = GetComponentInParent<PlayerMovement01>();
            playerMovement01.enabled = true;
        }
    
        void Start()
        {
            //health bar
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);

            //stamina bar
            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            stamina.SetMaxStamina(maxStamina);
        }

        void Update()
        {
            SprintCheck();
            // maybe put this in update!!!!!
            if(Input.GetKeyDown("r"))
            {
                HealBottle();
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void IncreaseMaxHealthLevel()
        {
            healthLevel++;
            maxHealth = healthLevel * 10;
            currentHealth = maxHealth;
        }

        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
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

        public void Healing(int healAmount)
        {
            if(currentHealth <= maxHealth)
            {
                currentHealth = currentHealth + healAmount;
                healthbar.SetCurrentHealth(currentHealth);
            }
        }

        private void HealBottle()
        {
            selfHealAmount = maxHealth / 5;

            if(currentHealth <= maxHealth && canHeal == true)
            {
                currentHealth = currentHealth + selfHealAmount;
                healthbar.SetCurrentHealth(currentHealth);

                canHeal = false;
                StartCoroutine(ResetHealBool());
            }
            
        }

        IEnumerator ResetHealBool()
        {
            yield return new WaitForSeconds(12);
            canHeal = true;
        }

        private void SprintCheck()
        {
            //if shift is pressed deduct stamina
            
            if(Input.GetKey(sprintKey))
            {
                //Debug.Log("check if descrease");
                stamina.DecreaseStamina();
                //stamina.SetCurrentStamina(currentStamina);
            }
            else 
            {
                //Debug.Log("check if Increase");
                stamina.IncreaseStamina();
                //stamina.SetCurrentStamina(currentStamina);
            }
        }
    }
}
