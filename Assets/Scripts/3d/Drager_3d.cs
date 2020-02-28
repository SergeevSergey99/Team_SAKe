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
        if (!unit.CompareTag("Actor") )
        {
            cost = 0;
            gameObject.transform.GetChild(0).GetComponent<Text>().text = "";
            reloadTime = 5000;
            return;
        }

        reloadTime = unit.GetComponent<Unit1_3d>().reloadTime;
        cost = unit.GetComponent<Unit1_3d>().cost;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
    }

    public Transform resourses;
    private Vector3 zeroPoint;
    private int cost = 0;
    public int reloadTime = 0;
    public int reloadTime_now = 0;
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
        if (res >= cost && Time.timeScale > 0 && reloadTime_now >= reloadTime)
        {
            reloadTime_now = 0;
            //создаём юнит в координатах где отпустили мышь
            if (!gameObject.name.Equals("AvaMe"))
            {
                Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                GameObject Unit = Instantiate(unit, new Vector3(9, 0.0f, (Camera.main.ScreenToWorldPoint(Input.mousePosition).y-3)*2),
                    Quaternion.Euler(0, 0, 0));
                
                Unit.GetComponent<Unit1_3d>().moveVector = Vector3.right;
                Unit.GetComponent<Unit1_3d>().isOurTeam = true;
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    
                    Instantiate(unit, new Vector3( rand.Next(-500, 500)/100.0f, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 35 + rand.Next(-500, 500)/100.0f, rand.Next(-500, 500)/100.0f),
                            Quaternion.Euler(0, 0, -90)).GetComponent<Rigidbody>().velocity = Vector3.down * 15;
                    

                }
            }

            res -= cost;
            resourses.GetComponent<Text>().text = res.ToString();
        }

        gameObject.GetComponent<RectTransform>().anchoredPosition = zeroPoint;
    }

    void FixedUpdate()
    {

        if (reloadTime >= reloadTime_now)
        {
            reloadTime_now++;
            gameObject.GetComponent<Image>().fillAmount = reloadTime_now / (float)reloadTime;
        }
    }
}