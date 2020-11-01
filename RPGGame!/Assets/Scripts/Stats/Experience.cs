using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;

        public event Action OnExperienceGained;
        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            OnExperienceGained();
        }
        public object CaptureState()
        {
            return experiencePoints;
        }
        public float GetPoints()
        {
            return experiencePoints;
        }
        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}