using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TowerHP;
    public GameObject EnemyNumber;
    public GameObject OperatorSetting;
    public static int passedEnemyNumber;
    public List<Image> operatorImg = new List<Image>();

    private bool onPause = false;
    private bool onDoubleSpeed = false;
    private int hp = 5;
    private int enemyNumber = 11;
    private int num = 0;
    Image Operbutton;
    void Start()
    {
        hp = 5;
        enemyNumber = 11;
        passedEnemyNumber = 0;
        foreach(Image setting in operatorImg)
        {
            Operbutton = Instantiate(setting,null);
            Operbutton.transform.SetParent(OperatorSetting.transform,false);
            Operbutton.transform.localPosition += new Vector3(-140*num, 0,0);
            num++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GF.SetTextMeshPro(TowerHP,$"{hp}");
        GF.SetTextMeshPro(EnemyNumber,$"{enemyNumber}/{passedEnemyNumber}");
        if (passedEnemyNumber == enemyNumber)
        {
            Time.timeScale = 0;
        }
    }

    public void OnPause()
    {
        if(onPause)
        {
            onPause = false;
            if (onDoubleSpeed)
            {
                Time.timeScale = 2;
            }
            else
            {
                Time.timeScale = 1;
            }
            
        }
        else
        {
            onPause = true;
            Time.timeScale = 0;
        }
        
    }
    public void OnDoubleSpeed()
    {
        if(onPause == false)
        {
            if (onDoubleSpeed)
            {
                onDoubleSpeed = false;
                Time.timeScale = 1;
            }
            else
            {
                onDoubleSpeed = true;
                Time.timeScale = 2;
            }
        }
        
    }

}
