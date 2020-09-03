﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        bool isDead = false;
        [SerializeField] float health = 15;
        public void HealthDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            Die();
        }

        private void Die()
        {
            if (health == 0)
            {
                if (isDead) return;
                    
                isDead = true;
                GetComponent<Animator>().SetTrigger("Die");
            }
        }
    }
}
            
