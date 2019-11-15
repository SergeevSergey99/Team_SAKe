using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class counter : MonoBehaviour
{
    public GameObject text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int cnt = 0;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        cnt++;
        text.GetComponent<Text>().text = cnt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
