using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

   // public String ArmyDescriptionText = "";
    [SerializeField] private GameObject CommanderProfilePanel;
    [SerializeField] private GameObject HeadquartersPanel;
    [SerializeField] private GameObject SkillDescription0;
    [SerializeField] private GameObject SkillDescription1;
    [SerializeField] private GameObject SkillDescription2;
    [SerializeField] private GameObject SkillDescription3;
    [SerializeField] private GameObject ArmySelectionPanel;
    
    // Start is called before the first frame update
    void Start()
    {
//        GameObject.Find("CommanderAvatar(Major)").GetComponent<Image>().sprite =
  //          GameManagerScript.GeneralSprite.GetComponent<Image>().sprite; 

        BackToCommanderProvifle();
    }
    
    public void SetActiveHeadquartersPanel()
    {
        CommanderProfilePanel.SetActive(false);
        HeadquartersPanel.SetActive(true);
        ArmySelectionPanel.SetActive(true);
    }

    public void BackToCommanderProvifle()
    {
        ArmySelectionPanel.SetActive(false);
        SkillDescription0.SetActive(false);
        SkillDescription1.SetActive(false);
        SkillDescription2.SetActive(false);
        SkillDescription3.SetActive(false);
        
        HeadquartersPanel.SetActive(false);
        CommanderProfilePanel.SetActive(true);
        CommanderProfilePanel.transform.Find("CommanderImage").GetComponent<Image>().sprite =
            GameManagerScript.General.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetActivArmySelection(Text text)
    {
        
        SkillDescription0.SetActive(false);
        SkillDescription1.SetActive(false);
        SkillDescription2.SetActive(false);
        SkillDescription3.SetActive(false);
        
        ArmySelectionPanel.SetActive(true);
        ArmySelectionPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = text.text;
    }
    
    public void SetActiveSkillDescription0()
    {
        ArmySelectionPanel.SetActive(false);
        
        SkillDescription1.SetActive(false);
        SkillDescription2.SetActive(false);
        SkillDescription3.SetActive(false);
        
        SkillDescription0.SetActive(true);
    }
    
    public void SetActiveSkillDescription1()
         {
             ArmySelectionPanel.SetActive(false);
             
             SkillDescription0.SetActive(false);
             SkillDescription2.SetActive(false);
             SkillDescription3.SetActive(false);
             
             SkillDescription1.SetActive(true);
         }
    
    public void SetActiveSkillDescription2()
    {
        ArmySelectionPanel.SetActive(false);
        
        SkillDescription0.SetActive(false);
        SkillDescription1.SetActive(false);
        SkillDescription3.SetActive(false);
        
        SkillDescription2.SetActive(true);
    }
    
    public void SetActiveSkillDescription3()
    {
        ArmySelectionPanel.SetActive(false);
        
        SkillDescription0.SetActive(false);
        SkillDescription1.SetActive(false);
        SkillDescription2.SetActive(false);
        
        SkillDescription3.SetActive(true);
    }
}
