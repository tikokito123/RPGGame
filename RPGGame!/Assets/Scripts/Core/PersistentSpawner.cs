using RPG.SceneManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class PersistentSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        static bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistent();
            hasSpawned = true;
        }

        private void SpawnPersistent()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
