﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System.Runtime.InteropServices;
using RPG.Core;
using RPG.Stats;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {

        [SerializeField] float health = 15;
        bool isDead = false;
        private void Start()
        {
            health = GetComponent<BaseStats>().GetHealth();
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void HealthDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            Die();
        }
        public float GetPercentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetHealth());
        }
        private void Die()
        {
            if (health == 0)
            {
                if (isDead) return;
                    
                isDead = true;
                GetComponent<Animator>().SetTrigger("Die");
                GetComponent<ActionShcelduler>().CancelCurrentAction();          
            }
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            Die();
        }
    }
}
            