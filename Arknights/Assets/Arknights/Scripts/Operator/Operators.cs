using System;
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
    [HideInInspector] public string operatorname;
    [HideInInspector] public int maxHP;
    [HideInInspector] public int HP;
    [HideInInspector] public int attack;
    [HideInInspector] public int defence;
    [HideInInspector] public int resist;
    [HideInInspector] public int ableToStop;
    [HideInInspector] public int Stop;
    [HideInInspector] public int maxSkillpoint;
    [HideInInspector] public int skillpoint;
    [HideInInspector] public float attackspeed;
    [HideInInspector] public float skillGuage;
    [HideInInspector] public bool firstSetting = true;
    [HideInInspector] public bool Isready = false;
    [HideInInspector] public bool onClick = false;
    [HideInInspector] public float PosX;
    [HideInInspector] public float PosY;
    [HideInInspector] public int direct = 0;
    [HideInInspector] public Transform range;
    public virtual void  Die()
    {
        
        UIManager.isDead = true;
        StopObjects.Clear();
        FoundObjects.Clear();
        gameObject.SetActive(false);
        button.SetActive(true);
        button.GetComponent<OperatorsButton>().onSetting = true;
        skillActive.SetActive(false);
        UIManager.active = true;
        UIManager.AbletoValue++;
    }
    public virtual void Despwan()
    {
        UIManager.isEscape = true;
        StopObjects.Clear();
        FoundObjects.Clear();
        gameObject.SetActive(false);
        button.SetActive(true);
        button.GetComponent<OperatorsButton>().onSetting = true;
        skillActive.SetActive(false);
        UIManager.active = true;
        UIManager.AbletoValue++;
        UIManager.costValue += (button.GetComponent<OperatorsButton>().Cost / 2);
    }
    
    public virtual void TilePosition()
    {

    }

    public virtual void Update()
    {
        if (firstSetting)
        {
            if(onClick)
            {
                if (Math.Abs(Input.mousePosition.x - PosX) > Math.Abs(Input.mousePosition.y - PosY))
                {
                    if (Input.mousePosition.x - PosX > 100)
                    {
                        switch (direct)
                        {
                            case 1:
                                ClearRight();
                                break;
                            case 2:
                                ClearLeft();
                                break;
                            case 3:
                                ClearUp();
                                break;
                            case 4:
                                ClearDown();
                                break;
                            default:
                                break;
                        }
                        direct = 1;
                        Right();
                    }
                    else if (Input.mousePosition.x - PosX < -100)
                    {
                        switch (direct)
                        {
                            case 1:
                                ClearRight();
                                break;
                            case 2:
                                ClearLeft();
                                break;
                            case 3:
                                ClearUp();
                                break;
                            case 4:
                                ClearDown();
                                break;
                            default:
                                break;
                        }

                        direct = 2;
                        Left();
                    }
                    else
                    {
                        switch (direct)
                        {
                            case 1:
                                ClearRight();
                                break;
                            case 2:
                                ClearLeft();
                                break;
                            case 3:
                                ClearUp();
                                break;
                            case 4:
                                ClearDown();
                                break;
                            default:
                                break;
                        }
                        direct = 0;
                    }
                }
                else
                {
                    if (Input.mousePosition.y - PosY > 100)
                    {
                        switch (direct)
                        {
                            case 1:
                                ClearRight();
                                break;
                            case 2:
                                ClearLeft();
                                break;
                            case 3:
                                ClearUp();
                                break;
                            case 4:
                                ClearDown();
                                break;
                            default:
                                break;
                        }
                        direct = 3;
                        Up();

                    }
                    else if (Input.mousePosition.y - PosY < -100)
                    {
                        switch (direct)
                        {
                            case 1:
                                ClearRight();
                                break;
                            case 2:
                                ClearLeft();
                                break;
                            case 3:
                                ClearUp();
                                break;
                            case 4:
                                ClearDown();
                                break;
                            default:
                                break;
                        }
                        direct = 4;
                        Down();
                    }
                    else
                    {
                        switch (direct)
                        {
                            case 1:
                                ClearRight();
                                break;
                            case 2:
                                ClearLeft();
                                break;
                            case 3:
                                ClearUp();
                                break;
                            case 4:
                                ClearDown();
                                break;
                            default:
                                break;
                        }
                        direct = 0;
                    }
                }
                if(Input.GetMouseButtonUp(0))
                {
                    onClick = false;
                    if (direct != 0)
                    {
                        if (UIManager.onDoubleSpeed)
                        {
                            Time.timeScale = 2;
                        }
                        else
                        {
                            Time.timeScale = 1;
                        }
                        button.SetActive(false);
                        UIManager.active = true;
                        UIManager.costValue -= button.GetComponent<OperatorsButton>().Cost;
                        UIManager.AbletoValue--;
                        gameObject.GetComponent<BoxCollider>().enabled = true;
                        firstSetting = false;
                    }

                    switch (direct)
                    {
                        case 1:
                            ClearRight();
                            range.eulerAngles = new Vector3(0f, 0f, 0f);
                            break;
                        case 2:
                            ClearLeft();
                            range.eulerAngles = new Vector3(0f, -180f, 0f);
                            break;
                        case 3:
                            ClearUp();
                            range.eulerAngles = new Vector3(0f, -90f, 0f);
                            break;
                        case 4:
                            ClearDown();
                            range.eulerAngles = new Vector3(0f, 90f, 0f);
                            break;
                        default:
                            break;
                    }

                }
            }
        }
    }
    public virtual void Right()
    {

        

    }
    public virtual void ClearRight()
    {
        
    }
    public virtual void Left()
    {
        

    }
    public virtual void ClearLeft()
    {
        
    }
    public virtual void Up()
    {
        

    }
    public virtual void ClearUp()
    {
        
    }
    public virtual void Down()
    {
        
    }
    public virtual void ClearDown()
    {
        
    }
}
