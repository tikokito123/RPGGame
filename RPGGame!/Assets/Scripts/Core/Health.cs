using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System.Runtime.InteropServices;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 15;
        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }
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
            
