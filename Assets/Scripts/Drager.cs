using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // Use this for initialization
    private void Start()
    {
        zeroPoint = gameObject.GetComponent<RectTransform>().anchoredPosition;
        if (!unit.CompareTag("Actor")) return;
        cost = unit.GetComponent<Unit1>().cost;
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

//    private Random rand;
    public void OnEndDrag(PointerEventData eventData)
    {
        int res = Int32.Parse(resourses.GetComponent<Text>().text);
        if (res >= cost && Time.timeScale > 0)
        {
            //создаём юнит в координатах где отпустили мышь
            
                Instantiate(unit, new Vector3(9, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -0.5f),
                    Quaternion.Euler(0, 0, 0));
            

            res -= cost;
            resourses.GetComponent<Text>().text = res.ToString();
        }

        gameObject.GetComponent<RectTransform>().anchoredPosition = zeroPoint;
    }
}