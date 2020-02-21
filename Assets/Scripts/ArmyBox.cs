using System.Collections.Generic;
using UnityEngine;

public class ArmyBox : MonoBehaviour
{
    [SerializeField] private GameObject General;
    [SerializeField] private List<GameObject> army;

    public void setArmy()
    {
        GameManagerScript.General = General;
        GameManagerScript.Army = army;
    }
}