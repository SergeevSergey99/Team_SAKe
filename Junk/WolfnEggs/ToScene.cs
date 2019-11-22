using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Final"))
        {
            StartCoroutine(QuiteGame());
            Debug.Log(SceneManager.GetSceneByName("Race").buildIndex);
        }
    }

    public int sceneName;
    
    IEnumerator ChangeScene(int index, float delay = 5f)
      {
          yield return new WaitForSeconds(delay);
          SceneManager.LoadScene(index);
      }  
    IEnumerator QuiteGame(float delay = 5f)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ChangeScene(sceneName, 2));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (sceneName != 3)
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.up * 0.6f;
        }

        if (transform.position.y <= 0)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up*2);
        }
    }
}
