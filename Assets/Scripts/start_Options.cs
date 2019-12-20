using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_Options : MonoBehaviour
{
    
    public GameObject General;
    public List<GameObject> Army;
    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript.General = General;
        GameManagerScript.Army = Army;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
