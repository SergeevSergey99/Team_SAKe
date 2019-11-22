using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class engine : MonoBehaviour
{
    // Start is called before the first frame
    // update

    public float angle;
    void Start()
    {
        float Speed = Random.Range(2f, 4f);
       // Debug.Log("Что записалось в rotate.y "+Math.Sin(transform.rotation.y));
        GetComponent<Rigidbody>().velocity = 
            new Vector3(
                (float)Math.Sin(angle), 
                0f,
                (float) -Math.Cos(angle)
                ).normalized * Speed;
    }

    private void OnDestroy()
    {
        GameObject go = GameObject.Find("Spamer");
            if(go != null)
                go.GetComponent<instatiator>().isDie();
    }

    // Update is called once per frame
    private int time = 1000;

    // Update is called once per frame
    void FixedUpdate()
    {
        time--;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
