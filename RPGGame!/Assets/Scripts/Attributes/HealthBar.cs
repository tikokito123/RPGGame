using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] Canvas rootCanvas = null;
        [SerializeField] RectTransform forground = null;
        void Update()
        {
            rootCanvas.enabled = true;
            CheckRootCanvas();
            forground.localScale = new Vector3(health.GetFranction(), 1, 1);
        }

        private void CheckRootCanvas()
        {
            if (Mathf.Approximately(health.GetFranction(), 0) || Mathf.Approximately(health.GetFranction(), 1))
            {
                rootCanvas.enabled = false;
                return;
            }
        }
    }
}
