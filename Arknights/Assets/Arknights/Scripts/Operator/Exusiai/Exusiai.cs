using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Exusiai : Operators
{
    [HideInInspector]
    public GameObject enemy;
    [HideInInspector]
    public string TagName;
    [HideInInspector]
    public float shortDis;
    public Image HPBar;
    public Image skillBar;
    public Transform range;
    public static bool skillready = false;

    private Animator animator;
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
        operatorname = "엑시아";
        maxHP = 10;
        HP = maxHP;
        attack = 540;
        defence = 161;
        resist = 0;
        ableToStop = 1;
        attackspeed = 1.3f;
        maxSkillpoint = 6;
    }
    public override void Despwan()
    {
        base.Despwan();
        button.GetComponent<OperatorsButton>().timer = 40; 
        Alive = true;
        firstSetting = true;
        HP = maxHP;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Stop = 0;
    }
    public override void Die()
    {
        base.Die();
        button.GetComponent<OperatorsButton>().timer = 40;
        Alive = true;
        firstSetting = true;
        HP = maxHP;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Stop = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (firstSetting)
            {
                if (OnClick == false)
                {
                    if (UIManager.onDoubleSpeed)
                    {
                        Time.timeScale = 2;
                    }
                    else
                    {
                        Time.timeScale = 1;
                    }
                    gameObject.SetActive(false);
                }
            }
        }
        HPguage = (float)HP / (float)maxHP;
        HPBar.fillAmount = HPguage;
        if (HP == 0)
        {
            if (Alive)
            {
                Alive = false;
                SelectOperator.selectedTarget = null;
                animator.SetTrigger("IsDie");
            }

        }
        skillGuage = (float)skillpoint / (float)maxSkillpoint;
        skillBar.fillAmount = skillGuage;
        if (skillGuage >= 1)
        {
            skillActive.SetActive(true);
            skillready = true; 
        }
        else
        {
            skillActive.SetActive(false);
        }
        if (StopObjects.Count != 0)
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
            if (StopObjects.Count != 0 || FoundObjects.Count != 0)
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
        if (enemi.HP > 0)
        {
            if(skillready)
            {
                
                for (int i=0;i<3;i++)
                {
                    enemi.HP--;
                }
                skillpoint = 0;
                skillready= false;
            }
            else
            {
                
                enemi.HP--;
                skillpoint++;
            }
            
        }
        else if (enemi.HP <= 0)
        {
            FoundObjects.Remove(enemy.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
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
            if (FoundObjects.Contains(other.gameObject))
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
            TileX = (int)ExusiaiButton.tileposition.position.x / 4;
            TileY = (int)ExusiaiButton.tileposition.position.z / 4;

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



    private void Right()
    {

        for (int i = 0; i < 4; i++)
        {

            for (int j = -1; j < 2; j++)
            {

                if (TileY + i < 10)
                {
                    //Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    RangeMap.Tile[TileX + j, TileY + i].gameObject.SetActive(true);
                }

            }

        }

    }
    private void ClearRight()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = -1; j < 2; j++)
            {

                if (TileY + i < 10)
                {
                    if (Map.Tile[TileX + j, TileY + i].tag == "Road" || Map.Tile[TileX + j, TileY + i].tag == "Wall")
                    {
                        //Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        //Map.Tile[TileX + j, TileY + i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    RangeMap.Tile[TileX + j, TileY + i].gameObject.SetActive(false);
                }
            }
        }
    }
    private void Left()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (TileY - i > -1)
                {
                    //Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    RangeMap.Tile[TileX + j, TileY - i].gameObject.SetActive(true);
                }
            }
        }

    }
    private void ClearLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            if (TileY - i > -1)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX + j, TileY - i].tag == "Road" || Map.Tile[TileX + j, TileY - i].tag == "Wall")
                    {
                        //Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        //Map.Tile[TileX + j, TileY - i].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    RangeMap.Tile[TileX + j, TileY - i].gameObject.SetActive(false);
                }
            }
            
        }
    }
    private void Up()
    {
        for (int i = 0; i < 4; i++)
        {
            if (TileX - i > -1)
            {
                for (int j = -1; j < 2; j++)
                {

                    //Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    RangeMap.Tile[TileX - i, TileY + j].gameObject.SetActive(true);
                }
            }
            
        }

    }
    private void ClearUp()
    {
        for (int i = 0; i < 4; i++)
        {
            if (TileX - i > -1)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX - i, TileY + j].tag == "Road" || Map.Tile[TileX - i, TileY + j].tag == "Wall")
                    {
                        //Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        //Map.Tile[TileX - i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    RangeMap.Tile[TileX - i, TileY + j].gameObject.SetActive(false);

                }
            }
            
        }
    }
    private void Down()
    {
        for (int i = 0; i < 4; i++)
        {
            if (TileX + i < 7)
            {
                for (int j = -1; j < 2; j++)
                {

                    //Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    RangeMap.Tile[TileX + i, TileY + j].gameObject.SetActive(true);
                }
            }
            

        }


    }
    private void ClearDown()
    {
        for (int i = 0; i < 4; i++)
        {
            if (TileX + i < 7)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (Map.Tile[TileX + i, TileY + j].tag == "Road" || Map.Tile[TileX + i, TileY + j].tag == "Wall")
                    {
                        //Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    else
                    {
                        //Map.Tile[TileX + i, TileY + j].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                    RangeMap.Tile[TileX + i, TileY + j].gameObject.SetActive(false);
                }
            }
            
        }
    }
}
