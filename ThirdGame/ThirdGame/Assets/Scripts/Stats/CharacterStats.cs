using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG 
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Health")]
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        private void OnDisable()
        {
            
        }
    }
}
