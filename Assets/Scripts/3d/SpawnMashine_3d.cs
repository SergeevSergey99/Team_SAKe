using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnMashine_3d : MonoBehaviour
{
    private int resourse = 10;

//	private float maxDistance = 8;
    private Vector3 moveVector;
  /*  public GameObject unitB;
    public GameObject unitZ;
    public GameObject unitK;
    public GameObject unitL;
*/
    public List<GameObject> unitList;
    int[] unitReloadlist = new int[100];
    int[] unitReloadlist_now = new int[100];
   /* private int unitB_reload;
    public int unitB_reload_now = 0;
    private int unitZ_reload;
    public int unitZ_reload_now = 0;
    private int unitK_reload;
    public int unitK_reload_now = 0;
    
    private int unitL_reload;
    
    
    public int unitL_reload_now = 0;
*/
    // Use this for initialization
    private Random rnd = new Random();

    void Start()
    {
        moveVector = gameObject.GetComponent<Unit1_3d>().GetMVector();/*
        maxValue = Mathf.Max(Mathf.Max(unitB.GetComponent<Unit1_3d>().cost, unitL.GetComponent<Unit1_3d>().cost),
            Mathf.Max(unitZ.GetComponent<Unit1_3d>().cost, unitK.GetComponent<Unit1_3d>().cost));
*/
        int i = 0;
        foreach (var unit in unitList)
        {
            maxValue = Mathf.Max(maxValue, unit.GetComponent<Unit1_3d>().cost);
            unitReloadlist[i] = unit.GetComponent<Unit1_3d>().reloadTime;
            unitReloadlist_now[i] = 0;
            i++;
        }
    /*    unitB_reload = unitB.GetComponent<Unit1_3d>().reloadTime;
        unitZ_reload = unitZ.GetComponent<Unit1_3d>().reloadTime;
        unitK_reload = unitK.GetComponent<Unit1_3d>().reloadTime;
        unitL_reload = unitL.GetComponent<Unit1_3d>().reloadTime;*/
    }

    public int reloadMax = 60;
    private int reload = 60;
    private int resReload = 100;

    private int maxValue;

    // Update is called once per frame
    void FixedUpdate()
    {

        for(int i = 0; i < unitList.Count-1; i++)
        {
            if (unitReloadlist_now[i] < unitReloadlist[i])
                unitReloadlist_now[i]++;
        }
/*
        if (unitB_reload_now < unitB_reload)
            unitB_reload_now++;
        if (unitK_reload_now < unitK_reload)
            unitK_reload_now++;
        if (unitL_reload_now < unitL_reload)
            unitL_reload_now++;
        if (unitZ_reload_now < unitZ_reload)
            unitZ_reload_now++;
  */      
        resReload--;
        if (resReload <= 0)
        {
            resourse++;
            resReload = 100;
        }

        if (reload <= 0)
        {
            if (resourse >= maxValue)
            {
                Collider[] clds = Physics.OverlapSphere(gameObject.transform.position + moveVector * 4.0f, 3.0f);
                int count = 0;
                float _z = 0;
                foreach (var colider in clds)
                {
                    if (colider.gameObject.CompareTag("Actor") &&
                        colider.gameObject.GetComponent<Unit1_3d>().isOurTeam ^
                        gameObject.GetComponent<Unit1_3d>().isOurTeam)
                    {
                        count++;
                        _z = colider.gameObject.transform.position.z;
                    }
                }

                float spawnZ;
                int unitType;

                if (count <= 0)
                {
                    unitType = rnd.Next(0, unitList.Count);
                    spawnZ = rnd.Next(-4, 5);
                    reloadMax = 60;
                }
                else
                {
                    reloadMax = 10;
                    resourse = maxValue;
                    unitType = (int)rnd.Next(0, unitList.Count);
                    Debug.Log(unitType);
                    spawnZ = _z;
                }

                if (unitReloadlist_now[unitType] >= unitReloadlist[unitType])
                {
                    unitReloadlist_now[unitType] = 0;
                    Instantiate(unitList[unitType], new Vector3(transform.position.x, 0, spawnZ + 0.01f+(Time.time%30/10000f)),
                        Quaternion.Euler(45, 0, 0));
                    reload = reloadMax;
                    resourse -= unitList[unitType].GetComponent<Unit1_3d>().cost;

                }
/*                switch (unitType)
                {
                    case 1:
                    {
                        if (unitK_reload_now >= unitK_reload)
                        {
                            unitB_reload_now = 0;
                            Instantiate(unitK, new Vector3(transform.position.x, 0, spawnZ + 0.01f+(Time.time%30/10000f)),
                                Quaternion.Euler(45, 0, 0));
                            reload = reloadMax;
                            resourse -= unitK.GetComponent<Unit1_3d>().cost;
                        }
                    }

                        break;
                    case 2:
                    {
                        if (unitB_reload_now >= unitB_reload)
                        {
                            unitB_reload_now = 0;
                            Instantiate(unitB, new Vector3(transform.position.x, 0, spawnZ + 0.01f +(Time.time%30/10000f)),
                                Quaternion.Euler(45, 0, 0));
                            reload = reloadMax;
                            resourse -= unitB.GetComponent<Unit1_3d>().cost;
                        }
                    }

                        break;
                    case 3:
                    {
                        if (unitZ_reload_now >= unitZ_reload)
                        {

                            unitZ_reload_now = 0;
                            Instantiate(unitZ, new Vector3(transform.position.x, 0, spawnZ + 0.01f+(Time.time%30/10000f)),
                                Quaternion.Euler(45, 0, 0));
                            reload = reloadMax;
                            resourse -= unitZ.GetComponent<Unit1_3d>().cost;
                        }
                    }
                        break;
                    case 4:
                    {
                        if (unitL_reload_now >= unitL_reload)
                        {

                            unitL_reload_now = 0;
                            Instantiate(unitL, new Vector3(transform.position.x, 0, spawnZ + 0.01f+(Time.time%30/10000f)),
                                Quaternion.Euler(45, 0, 0));
                            reload = reloadMax;
                            resourse -= unitL.GetComponent<Unit1_3d>().cost;
                        }
                    }
                        break;
                }*/
            }
        }
        else
        {
            reload--;
        }
    }
}