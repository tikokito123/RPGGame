using GameDevTV.Utils;
using System;
using System.Xml.Schema;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(0,99)]
        [SerializeField] int startingLevel = 0;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpPE = null;
        [SerializeField] bool shouldUseModifiers = false;

        public event Action onLevelUp;
        Experience experience;
        LazyValue<int> currentLevel;
        private void Awake()
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CauculateLevel);
        }
        private void OnEnable()
        {
            if (experience != null)
            {
                experience.OnExperienceGained += UpdateLevel;
            }
        }
        private void OnDisable()
        {
            if (experience != null)
            {
                experience.OnExperienceGained -= UpdateLevel;
            }
        }
        private void Start()
        {
            currentLevel.ForceInit();
        }
        private void UpdateLevel()
        {
            int newLevel = CauculateLevel();
            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }
        private void LevelUpEffect()
        {
            Instantiate(levelUpPE, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifiers(stat)) * (1 + GetPrecentageModifier(stat) / 100); 
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public int GetLevel()
        {
            return currentLevel.value;
        }
        private float GetAdditiveModifiers(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            float total = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;

        }
        private float GetPrecentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            float total = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                foreach (var modifier in provider.GetPersentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
        public int CauculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;
            float currentXP = experience.GetPoints();
            int penultimateLevels = progression.GetLevels(Stat.ExperienceLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevels; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceLevelUp, characterClass, level); 
                if (XPToLevelUp >= currentXP)
                {
                    return level;
                }
            }
            return penultimateLevels + 1;
        }
    }
}
