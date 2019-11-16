using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    public GameObject ps;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1)
        {
            var go = Instantiate(ps);
            go.transform.position = transform.position;
            go.GetComponent<ParticleSystem>().Play();
            
            Destroy(gameObject);
        }
    }

}