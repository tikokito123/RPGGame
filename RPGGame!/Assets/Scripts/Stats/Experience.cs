﻿using UnityEngine;
using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;


        public void GainExperience(float experience)
        {
            experiencePoints += experience;
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