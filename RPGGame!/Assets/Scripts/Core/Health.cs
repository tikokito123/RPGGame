using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }
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
                GetComponent<ActionShcelduler>().CancelCurrentAction();          
            }
        }
    }
}
            
