﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentlyActiveFade = null;
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }
       
        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }
        public Coroutine Fade(float target,float time)
        {
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentlyActiveFade;
        }
        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha,target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}

