using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyBox : MonoBehaviour
{
    [SerializeField] private GameObject General;
    [SerializeField] private List<GameObject> army;

    public void setArmy()
    {
        GameObject.Find("CommanderAvatar(Major)").GetComponent<Image>().sprite =
            gameObject.transform.Find("CommanderAvatar(Minor)").GetComponent<Image>().sprite; 
        GameManagerScript.General = General;
        GameManagerScript.Army = army;
    }
}