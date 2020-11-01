using UnityEngine;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] float health = 15;
        bool isDead = false;
        
        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            health = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }
        public void HealthDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + "took damage: " + damage);
            health = Mathf.Max(health - damage, 0);
            Die();
            AwardExperience(instigator);
        }

        public float GetHealth()
        {
            return health;
        }

        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetStat(Stat.Health));
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
        private void RegenerateHealth()
        {
            float regenerationHealth = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            health = Mathf.Max(health, regenerationHealth);
        }
        
        private void AwardExperience(GameObject instigator)
        {
            Experience xp = instigator.GetComponent<Experience>();
            if (xp == null) return;
            xp.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
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
            
