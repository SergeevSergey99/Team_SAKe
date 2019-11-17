using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Ruler : MonoBehaviour
{
    // Start is called before the first frame update
    private float startx = 0;
    public GameObject AR;
    void Start()
    {
        
    }

    private bool move = false;
    // Update is called once per frame
    private float x = 90;
    void Update()
    {/*
        if (Mathf.Abs(transform.rotation.z) > 60)
        {
            GetComponent<Rigidbody>().angularVelocity.Set(0,0, 
        }*/
      /*
        if (startx > AR.GetComponent<Transform>().position.x)
        {

            move = true;
            GetComponent<Rigidbody>().angularVelocity = Vector3.forward * 0.6f;
            startx = (int)AR.GetComponent<Transform>().position.x;
        }else if (startx < AR.GetComponent<Transform>().position.x)
        {
            move = true;
            GetComponent<Rigidbody>().angularVelocity = Vector3.forward * -0.6f;
            startx = (int)AR.GetComponent<Transform>().position.x;
        }
        if (Mathf.Abs(transform.rotation.z - 90) > 30f && move&& startx - transform.position.x<=0.1f)
        {
            Debug.Log("Back");
            move = false;
            Vector3 vel = GetComponent<Rigidbody>().angularVelocity;
            GetComponent<Rigidbody>().angularVelocity = - vel;

        }

        if (Mathf.Abs(transform.rotation.z - 90) < 1f && !move )
        {
            transform.rotation = Quaternion.Euler(90,180,90);
        }*/
      if (startx - transform.position.x > 0.1f)
      {


          if (Mathf.Abs(90 - x) < 15)
          {


              x += Time.deltaTime * 20;
              transform.rotation = Quaternion.Euler(x, 270, 90);
          }
          else
          {
              startx = transform.position.x;
              x -= Time.deltaTime * 20;
              transform.rotation = Quaternion.Euler(x, 270, 90);

          }
      }
      else if (transform.position.x - startx > 0.1f)
      {
          
          if (Mathf.Abs(90 - x) < 15)
          {


              x -= Time.deltaTime * 20;
              transform.rotation = Quaternion.Euler(x, 270, 90);
          }
          else
          {
              startx = transform.position.x;
              x += Time.deltaTime * 20;
              transform.rotation = Quaternion.Euler(x, 270, 90);

          }
      }
      else
      {
          if (x > 90)
          {
              x -= Time.deltaTime * 20;
          }else
              x += Time.deltaTime * 20;
          transform.rotation = Quaternion.Euler(x,270,90);
      }
    }
}
