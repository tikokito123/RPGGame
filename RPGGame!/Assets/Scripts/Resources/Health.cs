using UnityEngine;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {

        [SerializeField] float health = 15;
        bool isDead = false;
        private void Awake()
        {
            health = GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void HealthDamage(GameObject instigator, float damage)
        {
            health = Mathf.Max(health - damage, 0);
            Die();
            AwardExperience(instigator);
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
            
