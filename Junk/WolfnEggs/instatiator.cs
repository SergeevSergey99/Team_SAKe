using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class instatiator : MonoBehaviour
{
    public GameObject go;
    public GameObject text;
    public int timer = 1000;
    public float radius = 2;
    public bool isInArea = true;

    public int cnt = 15;

      IEnumerator ChangeScene(int index, float delay = 5f)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }

    public void isDie()
    {
        cnt--;
        if (cnt == 0)
        {




            text.GetComponent<Text>().text = "It is your Score";
            StartCoroutine(ChangeScene(0));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer--;
        if (timer <= 0)
        {
            timer = 500;
            if (isInArea)
            {
                Instantiate(go,
                    new Vector3(transform.position.x + Random.Range(-radius, radius), transform.position.y,
                        transform.position.z + Random.Range(-radius, radius)), Quaternion.identity);
            }
            else
            {
                float angle = Random.Range((float)-Math.PI, (float)Math.PI) ;
                var auto = Instantiate(go,
                    new Vector3(radius * (float)Math.Sin(angle), 0,
                        radius * (float)Math.Cos(angle)), Quaternion.Euler(0,angle*180/(float)Math.PI,0));
                auto.GetComponent<engine>().angle = -angle + (float)Math.PI/2;    
                
            }
        }

      
    }
}