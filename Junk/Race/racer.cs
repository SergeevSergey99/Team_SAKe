using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class racer : MonoBehaviour
{
    private GameObject text;
    private GameObject Endtext;
 //   public GameObject block;

    public bool isBoarder = false;

    // Start is called before the first frame update
    void Start()
    {
        name = name.Replace("(Clone)","");
        text = GameObject.Find("Score");
        Endtext = GameObject.Find("Text");
        GetComponent<Rigidbody>().velocity = Vector3.back * (isBoarder ? 100 : 15);
    }

    private void OnTriggerEnter(Collider other)
    {

        int mod = 0;
        int len = 220;
       
       

        
        if (!isBoarder)
        {
            int a;
            do
            {

                mod = Random.Range(-1, 2);
                len++;

                Collider[] clds = Physics.OverlapBox(
                    Vector3.back * 15 + Vector3.forward * len + Vector3.left * mod * 10,
                    Vector3.one * 2);
                a = clds.Length;
            } while (a != 0);

            
            int tmp = Int32.Parse(text.GetComponent<Text>().text);
            tmp++;
            text.GetComponent<Text>().text = tmp.ToString();
        }
        
        
        Instantiate(gameObject, (isBoarder?transform.position: Vector3.back*15) + Vector3.forward * len + Vector3.left * mod * 10, Quaternion.Euler(0,180,0));
        
        Destroy(gameObject);
    }
    
    IEnumerator ChangeScene(int index, float delay = 5f)
    {
        yield return new WaitForSeconds(delay);
        
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
    private void OnCollisionEnter(Collision other)
    {
        
        Time.timeScale = 0.01f;
        Endtext.GetComponent<Text>().text = "Your score is";
        StartCoroutine(ChangeScene(0, 0.05f));
    }

    
    // Update is called once per frame
    void Update()
    {
    }
}