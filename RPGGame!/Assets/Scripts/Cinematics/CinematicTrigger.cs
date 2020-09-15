using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool IsTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && !IsTriggered)
            {
                GetComponent<PlayableDirector>().Play();
                IsTriggered = true;
            }
        }
        public object CaptureState()
        {
            return IsTriggered;
        }

        public void RestoreState(object state)
        {
            IsTriggered = (bool)state;
        }
    }
}
