using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] RectTransform forground = null;
        void Update()
        {
            forground.localScale = new Vector3(health.GetFranction(), 1, 1);
        }
    }
}
