using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookUpTable = null;
        public float GetStat(Stat stat,CharacterClass characterClass,int Level)
        {
            BuildLookUp();
            float[] levels = lookUpTable[characterClass][stat];
            if (levels.Length < Level) return 0;
            return levels[Level];
            /*
            foreach (progressioncharacterclass progressionclass in characterclasses)
            {
                if (progressionclass.characterclass != characterclass) continue;
                foreach (progressionstat progressionstat in progressionclass.stats)
                {
                    if (progressionstat.stat != stat) continue;
                    if (level > progressionstat.levels.length) continue;
                    return progressionstat.levels[level - 1];
                }
            }
            return 0;
           */
        }

        private void BuildLookUp()
        {
            if (lookUpTable != null) return;
            lookUpTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookUpTable = new Dictionary<Stat,float[]>();
                foreach (var progressionStat in progressionClass.stats)
                {
                    statLookUpTable[progressionStat.stat] = progressionStat.levels;
                }
                lookUpTable[progressionClass.characterClass] = statLookUpTable;     
            }
        }
        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookUp();
            float[] levels = lookUpTable[characterClass][stat];
            return levels.Length;
        }
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
            //public float[] health;
        }
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}
