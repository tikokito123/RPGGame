﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
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
    }
}
