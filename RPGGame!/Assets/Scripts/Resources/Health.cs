using UnityEngine;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using GameDevTV.Utils;
using UnityEngine.Events;
using RPG.UI.DamageText;
using System.Runtime.InteropServices.WindowsRuntime;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;
        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }
        LazyValue<float> health;
        bool isDead = false;
        

        private void Awake()
        {
            health = new LazyValue<float>(GetInitialHealth);
        }
        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        private void Start()
        {
            health.ForceInit();
            
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }
        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void HealthDamage(GameObject instigator, float damage)
        {
            health.value = Mathf.Max(health.value - damage, 0);
            if (health.value <= 0)
            {
                Die();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }
        public float GetHealth()
        {
            return health.value;
        }

        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (health.value / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionShcelduler>().CancelCurrentAction();          
        }
        private void RegenerateHealth()
        {
            float regenerationHealth = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            health.value = Mathf.Max(health.value, regenerationHealth);
        }
        
        private void AwardExperience(GameObject instigator)
        {
            Experience xp = instigator.GetComponent<Experience>();
            if (xp == null) return;
            xp.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public object CaptureState()
        {
            return health.value;
        }

        public void RestoreState(object state)
        {
            health.value = (float)state;
            if (health.value <= 0)
            {
                Die();
            }
        }
    }
}
            
