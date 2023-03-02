using System;
using UnityEngine;
using UnityEngine.UI;

public class SilverAsh : Operators
{


    [HideInInspector]
    public GameObject enemy;
    [HideInInspector]
    public string TagName;
    [HideInInspector]
    public float shortDis;
    public Image HPBar;
    public Image skillBar;
    

    private Animator animator;
    private bool Alive = true;
    private int TileX;
    private int TileY;
    private float HPguage;

    void Awake()
    {
        firstSetting = true;
        animator = GetComponentInChildren<Animator>();
        operatorname = "실버애쉬";
        maxHP = 10;
        HP = maxHP;
        attack = 713;
        defence = 397;
        resist = 10;
        ableToStop = 2;
        attackspeed = 1.3f;
        skillGuage = 0;
        maxSkillpoint = 15;
    }
    public override void Despwan()
    {
        base.Despwan();
        button.GetComponent<OperatorsButton>().timer = 60;
        Alive = true;
        firstSetting = true;
        HP = maxHP;
        
        Stop = 0;
        skillGuage = 0;
    }
    public override void Die()
    {
        base.Die();
        button.GetComponent<OperatorsButton>().timer = 60;
        Alive = true;
        firstSetting = true;
        HP = maxHP;
        
        Stop = 0;
        skillGuage = 0;
    }
    public override void Update()
    {
        base.Update();
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
            enemi.HP--;
            skillpoint++;
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


    public override void TilePosition()
    {
        base.TilePosition();
        PosX = Input.mousePosition.x;
        PosY = Input.mousePosition.y;
        TileX = (int)SilverAshButton.tileposition.position.x / 4;
        TileY = (int)SilverAshButton.tileposition.position.z / 4;

    }
    
    public override void Right()
    {

        for (int i = 0; i < 4; i++)
        {

            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {
                    RangeMap.Tile[TileX + j, TileY + i].gameObject.SetActive(true);
                }
            }
            else
            {
                if (TileY + i < 10)
                {
                    RangeMap.Tile[TileX, TileY + i].gameObject.SetActive(true);
                }
            }
        }

    }
    public override void ClearRight()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {
                    RangeMap.Tile[TileX + j, TileY + i].gameObject.SetActive(false);
                }
            }
            else
            {
                if (TileY + i < 10)
                {
                    RangeMap.Tile[TileX, TileY + i].gameObject.SetActive(false);
                }
            }
        }
    }
    public override void Left()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {
                    RangeMap.Tile[TileX + j, TileY - i].gameObject.SetActive(true);
                }
            }
            else
            {
                if (TileY - i > -1)
                {
                    RangeMap.Tile[TileX, TileY - i].gameObject.SetActive(true);
                }
            }
        }

    }
    public override void ClearLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {
                    RangeMap.Tile[TileX + j, TileY - i].gameObject.SetActive(false);
                }
            }
            else
            {
                if (TileY - i > -1)
                {
                    RangeMap.Tile[TileX, TileY - i].gameObject.SetActive(false);
                }

            }
        }
    }
    public override void Up()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {
                    RangeMap.Tile[TileX - i, TileY + j].gameObject.SetActive(true);
                }
            }
            else
            {
                if (TileX - i > -1)
                {
                    RangeMap.Tile[TileX - i, TileY].gameObject.SetActive(true);
                }
            }
        }

    }
    public override void ClearUp()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {
                    RangeMap.Tile[TileX - i, TileY + j].gameObject.SetActive(false);
                }
            }
            else
            {
                if (TileX - i > -1)
                {
                    RangeMap.Tile[TileX - i, TileY].gameObject.SetActive(false);
                }

            }
        }
    }
    public override void Down()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {
                    RangeMap.Tile[TileX + i, TileY + j].gameObject.SetActive(true);
                }
            }
            else
            {
                if (TileX + i < 7)
                {
                    RangeMap.Tile[TileX + i, TileY].gameObject.SetActive(true);
                }
            }

        }


    }
    public override void ClearDown()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                for (int j = -1; j < 2; j++)
                {
                    RangeMap.Tile[TileX + i, TileY + j].gameObject.SetActive(false);
                }
            }
            else
            {
                if (TileX + i < 7)
                {
                    RangeMap.Tile[TileX + i, TileY].gameObject.SetActive(false);
                }

            }
        }
    }


}
