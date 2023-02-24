using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Operators : MonoBehaviour
{
    public Sprite illust;
    public Sprite skill;
    public GameObject button;
    public GameObject skillActive;

    [HideInInspector] public List<GameObject> StopObjects;
    [HideInInspector] public List<GameObject> FoundObjects;
    [HideInInspector] public int maxHP;
    [HideInInspector] public int HP;
    [HideInInspector] public int attack;
    [HideInInspector] public int ableToStop;
    [HideInInspector] public int Stop;
    [HideInInspector] public int maxSkillpoint;
    [HideInInspector] public int skillpoint;
    [HideInInspector] public float attackspeed;
    [HideInInspector] public float skillGuage;
    [HideInInspector] public bool firstSetting = true;
    public virtual void  Die()
    {
        StopObjects.Clear();
        FoundObjects.Clear();
        gameObject.SetActive(false);
        button.SetActive(true);
        skillActive.SetActive(false);
        UIManager.AbletoValue++;
    }
    public virtual void Despwan()
    {
        StopObjects.Clear();
        FoundObjects.Clear();
        gameObject.SetActive(false);
        button.SetActive(true);
        skillActive.SetActive(false);
        UIManager.AbletoValue++;
        UIManager.costValue += (button.GetComponent<OperatorsButton>().Cost / 2);
    }
    
    
}
