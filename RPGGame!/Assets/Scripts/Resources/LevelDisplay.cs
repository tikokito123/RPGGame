using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    BaseStats level;
    void Awake()
    {
        level = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
    }
    void Update()
    {
        GetComponent<Text>().text = String.Format("{0:0}", level.GetLevel());
    }
}
