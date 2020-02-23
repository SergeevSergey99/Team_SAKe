using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArmy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject.Find("Unit_Main").GetComponent<SpriteRenderer>().sprite = GameManagerScript.General.GetComponent<SpriteRenderer>().sprite;
        GameObject.Find("Unit_Main").GetComponent<SpriteRenderer>().flipX = !GameManagerScript.General.GetComponent<SpriteRenderer>().flipX;
        GameObject.Find("Unit_Main").GetComponent<Animator>().runtimeAnimatorController = GameManagerScript.General.GetComponent<Animator>().runtimeAnimatorController;
        GameObject.Find("Unit_Main").GetComponent<Unit1_3d>().health = GameManagerScript.General.GetComponent<Unit1_3d>().health;
        GameObject.Find("Unit_Main").GetComponent<Unit1_3d>().damage = GameManagerScript.General.GetComponent<Unit1_3d>().damage;
        GameObject.Find("Unit_Main").GetComponent<Unit1_3d>().reloadTime = GameManagerScript.General.GetComponent<Unit1_3d>().reloadTime;
        GameObject.Find("Unit_Main").GetComponent<AudioSource>().clip = GameManagerScript.General.GetComponent<AudioSource>().clip;
        GameObject.Find("Unit_Main").GetComponent<Unit1_3d>().maxDistance = GameManagerScript.General.GetComponent<Unit1_3d>().maxDistance;
        GameObject.Find("Unit_Main").GetComponent<Unit1_3d>().bulletPrefab = GameManagerScript.General.GetComponent<Unit1_3d>().bulletPrefab;
        GameObject.Find("Unit_Main").GetComponent<Unit1_3d>().isMeele = GameManagerScript.General.GetComponent<Unit1_3d>().isMeele;
        GameObject.Find("Unit_Main").GetComponent<SpawnMashine_3d>().unitList = GameManagerScript.Army;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
