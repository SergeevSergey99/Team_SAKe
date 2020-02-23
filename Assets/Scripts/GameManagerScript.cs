using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    //public static int points = 100;
    public static bool isSet = false;
    public static GameObject General;
    public static List<GameObject> Army;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetGeneralAndArmy(GameObject G, List<GameObject> A)
    {
        General = G;
        Army = A;
    }
}