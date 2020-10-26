using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(0,99)]
        [SerializeField] int startingLevel = 0;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        private void Update()
        {
            if (gameObject.tag == "Player")
            {
                print(GetLevel());
            }
        }
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }
        public int GetLevel()
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
