using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        PlayableDirector playableDirector;
        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.played += EnableControl;
            playableDirector.stopped += DisableControl;
            player = GameObject.FindGameObjectWithTag("Player");
        }
        void DisableControl(PlayableDirector playableDirector)
        {
            
            player.GetComponent<ActionShcelduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector playableDirector)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

    }
}