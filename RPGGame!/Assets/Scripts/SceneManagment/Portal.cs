using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    enum DestinationIdentifier
    {
        A,B,C,D,E,F,G
    }
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = 0;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float waitTime = 0.5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }
        private void Start()
        {
        }
        private IEnumerator Transition()
        {
            SavingWrapper Wrapper = FindObjectOfType<SavingWrapper>();
            Fader fader = FindObjectOfType<Fader>();
            if (sceneToLoad < 0)
            {
                Debug.LogError("sceneToLoad not set");
                yield break;
            }
            DontDestroyOnLoad(gameObject);
            yield return fader.FadeOut(fadeOutTime);
            Wrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Wrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            Wrapper.Save();
            yield return new WaitForSeconds(waitTime);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                    return portal;
            }
            return null;
        }
    }
}