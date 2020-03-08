using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetArmy : MonoBehaviour
{
    [SerializeField]private GameObject UI_Unit;
    // Start is called before the first frame update
    void Awake()
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

        for (int i = 0; i < GameManagerScript.Army.Count; i++)
        {
            GameObject icon = Instantiate(UI_Unit, GameObject.Find("Canvas UI").transform);
            icon.name = "Unit" + i;
            icon.GetComponent<Image>().sprite = GameManagerScript.Army[i].GetComponent<SpriteRenderer>().sprite;
            icon.transform.GetChild(0).GetComponent<Drager_3d>().resourses =
                GameObject.Find("Canvas UI").transform.Find("Resources").GetChild(0);
            icon.transform.GetChild(0).GetComponent<Image>().sprite = GameManagerScript.Army[i].GetComponent<SpriteRenderer>().sprite;
            icon.transform.GetChild(0).GetComponent<Drager_3d>().unit = GameManagerScript.Army[i];
            
            icon.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-170 * i - 10,0,0);
            //SetPositionAndRotation(new Vector3(-170 * i - 10,0,0), Quaternion.Euler(0,0,0) );
            //position.Set(  -170 * i - 10,0,0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
