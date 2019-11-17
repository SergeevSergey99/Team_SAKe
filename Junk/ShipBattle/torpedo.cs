using System;
using UnityEngine;
using UnityEngine.UI;

public class torpedo : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ps;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        var go = Instantiate(ps);
        go.transform.position = transform.position;
        go.GetComponent<ParticleSystem>().Play();

        if (other.gameObject.CompareTag("Ship"))
        {
            var score = GameObject.Find("Score");
            int tmp = Int32.Parse(score.GetComponent<Text>().text);
            tmp++;
            score.GetComponent<Text>().text = tmp.ToString();
        }

        Destroy(gameObject);
    }

    void Start()
    {
    }

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