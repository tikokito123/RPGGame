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

        public event Action onLevelUp;

        int currentLevel = 0;

        private void Start()
        {
            Experience experience = GetComponent<Experience>();
            currentLevel = CauculateLevel();
            if (experience != null)
            {
                experience.OnExperienceGained += UpdateLevel;
            }
        }
        private void UpdateLevel()
        {
            int newLevel = CauculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
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
            return progression.GetStat(stat, characterClass, startingLevel) + GetAdditiveModifiers(stat);
        }
        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                return currentLevel = CauculateLevel();
            }
            return currentLevel;
        }
        private float GetAdditiveModifiers(Stat stat)
        {
            float total = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifier(stat))
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
            for (int level = 0; level <= penultimateLevels; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceLevelUp, characterClass, level); 
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLevels + 1;
        }
    }
}
