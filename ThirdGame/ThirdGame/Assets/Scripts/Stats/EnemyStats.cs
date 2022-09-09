using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EG 
{
    public class EnemyStats : CharacterStats
    {
        public GameObject Skelett;
        
        public GameObject GameManager;
        public EnemyAi enemyAi;
        Collider capCollider;

        [SerializeField] private Slider EnemyHealthbarSlider;
        [SerializeField] private GameObject FloatingDamageText;
        //[SerializeField] private GameObject wh;
        //[SerializeField] private WeaponController01 wc;

        public float waitTime;


        private void Awake()
        {
            enemyAi = GetComponentInParent<EnemyAi>();
            enemyAi.enabled = true;

            // bug fix for infinite score
            capCollider = GetComponent<Collider>();
        }
    
        void Start()
        {
            // actual health
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;

            SetHealthbarUi();
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;

            // slider healthbar set
            SetMaxHealth(maxHealth);

            return maxHealth;
        }

        public void IncreaseMaxHealthLevel()
        {
            healthLevel++;
            maxHealth = healthLevel * 10;
        }

        public void TakeDamage(int damage)
        {
            //deduct health
            currentHealth = currentHealth - damage;
            //damage text + fix double text pop up
            //wc = GetComponent<WeaponController01>();
            //wc.CanAttack = false;
            Instantiate(FloatingDamageText, transform.position, Quaternion.identity).GetComponent<DamageText>().Initialise(damage);
            //set enemy healthbar
            SetHealthbarUi();
            //anim
            Skelett.GetComponent<Animator>().SetTrigger("isHurt");

            // death anim
            if(currentHealth <= 0) 
            {
                currentHealth = 0;

                // Death anim + bug fix and destroy for death
                Skelett.GetComponent<Animator>().SetBool("isDead", true);
                capCollider.enabled = false;
                enemyAi.enabled = false;
                Object.Destroy(Skelett, waitTime);
            }
        }

        public void SetHealthbarUi()
        {
            EnemyHealthbarSlider.value = currentHealth;
        }

        public void SetMaxHealth(int maxHealth)
        {
            EnemyHealthbarSlider.maxValue = maxHealth;
            EnemyHealthbarSlider.value = maxHealth;
        }
    }
}
