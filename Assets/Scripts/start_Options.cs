using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start_Options : MonoBehaviour
{
    public GameObject General;
    public SpriteRenderer GeneralSprite;

    public List<GameObject> Army;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameManagerScript.isSet)
        {
            GameManagerScript.General = General;
        //    GameManagerScript.GeneralSprite = GeneralSprite;
            GameManagerScript.Army = Army;
            GameManagerScript.isSet = true;
        }
    }
}