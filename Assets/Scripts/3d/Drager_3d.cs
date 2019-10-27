using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class Drager_3d : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // Use this for initialization
    private void Start()
    {
        zeroPoint = gameObject.GetComponent<RectTransform>().anchoredPosition;
        if (!unit.CompareTag("Actor"))
        {
            cost = 10;
            gameObject.transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
            return;
        }
        cost = unit.GetComponent<Unit1_3d>().cost;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
    }

    public GameObject resourses;
    private Vector3 zeroPoint;
    private int cost = 0;
    public GameObject unit;

    public void OnDrag(PointerEventData eventData)
    {
//        float k = Screen.currentResolution.height / 733.0f;
        float k = 1.0f;
        //наводим курором на место где должен появиться юнит
        gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(0, (Input.mousePosition.y) * k, 0);
    }

    private Random rand = new Random();
    public void OnEndDrag(PointerEventData eventData)
    {
        int res = Int32.Parse(resourses.GetComponent<Text>().text);
        if (res >= cost && Time.timeScale > 0)
        {
            //создаём юнит в координатах где отпустили мышь
            if (!gameObject.name.Equals("AvaMe"))
            {
                Instantiate(unit, new Vector3(9, 0.0f, Camera.main.ScreenToWorldPoint(Input.mousePosition).y),
                    Quaternion.Euler(45, 0, 0));
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    
                    Instantiate(unit, new Vector3( rand.Next(-500, 500)/100.0f, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 35 + rand.Next(-500, 500)/100.0f, rand.Next(-500, 500)/100.0f),
                            Quaternion.Euler(45, 0, -90)).GetComponent<Rigidbody>().velocity = Vector3.down * 15;

                }
            }

            res -= cost;
            resourses.GetComponent<Text>().text = res.ToString();
        }

        gameObject.GetComponent<RectTransform>().anchoredPosition = zeroPoint;
    }
}