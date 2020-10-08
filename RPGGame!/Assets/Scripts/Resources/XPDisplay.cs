using UnityEngine;
using UnityEngine.UI;
using RPG.Resources;

public class XPDisplay : MonoBehaviour
{
    Experience XP;
    private void Awake()
    {
        XP = GameObject.FindWithTag("Player").GetComponent<Experience>();
    }
    private void Update()
    {
        GetComponent<Text>().text = string.Format("{0:0}", XP.GetPoints().ToString());
    }
}
