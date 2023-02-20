using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilverAsh : Operators
{


   
    public GameObject enemy;
    public string TagName;
    public float shortDis;
    public Image HPBar;
    public Transform range;
        
    private  Animator animator;
    private bool OnClick = false;
    private bool Alive = true;
    private float PosX;
    private float PosY;
    private int TileX;
    private int TileY;
    private int direct = 0;
    private float HPguage;
    void Awake()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        animator = GetComponentInChildren<Animator>();
        maxHP = 10;
        HP = maxHP;
        attack = 1;
        defence = 1;
        resist = 10;
        respawn = 70;
        spawnCost = 19;
        ableToStop = 2;
        attackspeed = 1.3f;
    }
    public override void Despwan()
    {
        base.Despwan();
        Alive = true;
        firstSetting = true;
        HP = maxHP;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Stop = 0;
    }
    public override void Die()
    {
        base.Die();
        Alive = true;
        firstSetting = true;
        HP= maxHP;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Stop = 0;
    }
    private void Update()
    {
        HPguage = (float)HP / (float)maxHP;
        HPBar.fillAmount= HPguage;
        if (HP == 0)
        {
            if(Alive)
            {
                Alive= false;
                SelectOperator.selectedTarget = null;
                animator.SetTrigger("IsDie");
            }
            
        }
        if(StopObjects.Count !=0)
        {
            enemy = StopObjects[0];
        }
        else
        {
            shortDis = Mathf.Infinity; // 첫번째를 기준으로 잡아주기 
            foreach (GameObject found in FoundObjects)
            {
                if (found != null)
                {
                    float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                    if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                    {
                        enemy = found;
                        shortDis = Distance;
                    }
                }

            }
            if (shortDis == Mathf.Infinity)
            {
                enemy = null;
            }
            
            
        }

        if (firstSetting == false)
        {
            animator.SetBool("SettingOn", true);
            if (StopObjects.Count !=0 || FoundObjects.Count != 0)
            {
                if (enemy.GetComponent<Enemy>().HP <= 0)
                {
                    FoundObjects.Remove(enemy.gameObject);
                }
                animator.SetBool("IsAttack", true);
            }
            else
            {
                animator.SetBool("IsAttack", false);
            }
        }
        
    }

    public void attackenemy()
    {
        Enemy enemi = enemy.GetComponent<Enemy>();
        if(enemi.HP > 0 )
        {
            enemi.HP--;
        }
        else if(enemi.HP <= 0)
        {
            FoundObjects.Remove(enemy.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if (!FoundObjects.Contains(other.gameObject))
            {
                FoundObjects.Add(other.gameObject);
            }
           
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(FoundObjects.Contains(other.gameObject))
            {
                FoundObjects.Remove(other.gameObject);
            }
            
        }
    }


    private void OnMouseDown()
    {
        if (firstSetting)
        {
            OnClick = true;
            PosX = Input.mousePosition.x;
            PosY = Input.mousePosition.y;
            TileX = (int)SilverAshButton.tileposition.position.x / 4;
            TileY = (int)SilverAshButton.tileposition.position.z / 4;

        }
    }
    private void OnMouseDrag()
    {
        if (OnClick)
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
        }
    }
    private void OnMouseUp()
    {
        if (firstSetting)
        {
            OnClick = false;
            
            if(direct != 0)
            {
                Time.timeScale = 1;
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



    private void Right()
    {

        for (int i = 0; i < 4; i++)
        {

            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.magenta;

                }
            }
            else
            {
                if (TileY + i < 10)
                {
                    Map.Tile[TileX, TileY + i].GetComponent<MeshRenderer>().material.color = Color.magenta;
                }
            }
        }

    }
    private void ClearRight()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX + j, TileY + i].tag == "Road" || Map.Tile[TileX + j, TileY + i].tag == "Wall")
                    {
                        Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }

                }
            }
            else
            {
                if (TileY + i < 10)
                {
                    if (Map.Tile[TileX, TileY + i].tag == "Road" || Map.Tile[TileX, TileY + i].tag == "Wall")
                    {
                        Map.Tile[TileX, TileY + i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX, TileY + i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }
            }
        }
    }
    private void Left()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.magenta;

                }
            }
            else
            {
                if (TileY - i > -1)
                {
                    Map.Tile[TileX, TileY - i].GetComponent<MeshRenderer>().material.color = Color.magenta;
                }
            }
        }

    }
    private void ClearLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX + j, TileY - i].tag == "Road" || Map.Tile[TileX + j, TileY - i].tag == "Wall")
                    {
                        Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }

                }
            }
            else
            {
                if (TileY - i > -1)
                {
                    if (Map.Tile[TileX, TileY - i].tag == "Road" || Map.Tile[TileX, TileY - i].tag == "Wall")
                    {
                        Map.Tile[TileX, TileY - i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX, TileY - i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }

            }
        }
    }
    private void Up()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.magenta;

                }
            }
            else
            {
                if (TileX - i > -1)
                {
                    Map.Tile[TileX - i, TileY].GetComponent<MeshRenderer>().material.color = Color.magenta;
                }
            }
        }

    }
    private void ClearUp()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX - i, TileY + j].tag == "Road" || Map.Tile[TileX - i, TileY + j].tag == "Wall")
                    {
                        Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.black;
                    }


                }
            }
            else
            {
                if (TileX - i > -1)
                {
                    if (Map.Tile[TileX - i, TileY].tag == "Road" || Map.Tile[TileX - i, TileY].tag == "Wall")
                    {
                        Map.Tile[TileX - i, TileY].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX - i, TileY].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }

            }
        }
    }
    private void Down()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.magenta;

                }
            }
            else
            {
                if (TileX + i < 7)
                {
                    Map.Tile[TileX + i, TileY].GetComponent<MeshRenderer>().material.color = Color.magenta;
                }
            }

        }


    }
    private void ClearDown()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX + i, TileY + j].tag == "Road" || Map.Tile[TileX + i, TileY + j].tag == "Wall")
                    {
                        Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.black;
                    }

                }
            }
            else
            {
                if (TileX + i < 7)
                {
                    if (Map.Tile[TileX + i, TileY].tag == "Road" || Map.Tile[TileX + i, TileY].tag == "Wall")
                    {
                        Map.Tile[TileX + i, TileY].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        Map.Tile[TileX + i, TileY].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }

            }
        }
    }


}
