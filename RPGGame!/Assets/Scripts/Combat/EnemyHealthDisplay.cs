using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }
        private void Update()
        {
            if (!fighter.GetTarget())
            {
                GetComponent<Text>().text = "N/A";
                return;
            }
            GetComponent<Text>().text = string.Format("{0:0}/{1:0}", fighter.GetTarget().GetHealth(), fighter.GetTarget().GetMaxHealth());
        }
    }
}

