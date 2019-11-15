using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instatiator : MonoBehaviour
{
    
    
    public GameObject go;
    public int timer = 1000;

    public int cnt = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer--;
        if (timer <= 0)
        {
            cnt--;
            timer = 500;
            Instantiate(go, 
                new Vector3(transform.position.x + Random.Range(-5.0f, 5.0f), transform.position.y, 
                    transform.position.z + Random.Range(-5.0f, 5.0f)),Quaternion.identity);
            
        }

        if (cnt <= 0)
        {
            Destroy(gameObject);
        }
    }
}
