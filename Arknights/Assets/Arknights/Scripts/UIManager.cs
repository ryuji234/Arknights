using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TowerHP;
    public GameObject EnemyNumber;
    public GameObject OperatorSetting;
    public GameObject Cost;
    public GameObject AbletoSetting;
    public GameObject missionabnner;
    public GameObject missionAccomplished;
    public GameObject missionFailed;
    public GameObject resultBanner;
    public GameObject FadePannel;
    public Image CostGuage;
    public static int hp = 5;
    public static int passedEnemyNumber;
    public static int costValue;
    public static int AbletoValue;
    public static bool onDoubleSpeed = false;
    public List<Image> operatorImg = new List<Image>();

    private List<Image> operatorImgSet = new List<Image>();
    private bool onPause = false;
    private bool allSet = false;
    private bool start = true;
    private int num = 0;
    private int enemyNumber = 11;
    private float time = 0;
    private int speed = 30;
    private float timer = 0;
    private bool click;
    void Start()
    {
        click = true;
        missionabnner.SetActive(false);
        missionAccomplished.SetActive(false);
        missionFailed.SetActive(false);
        resultBanner.SetActive(false);
        hp = 5;
        enemyNumber = 11;
        costValue = 11;
        passedEnemyNumber = 0;
        AbletoValue = 8;
        foreach (Image setting in operatorImg)
        {
            Image Operbutton = Instantiate(setting, null);
            Operbutton.transform.SetParent(OperatorSetting.transform, false);
            Operbutton.transform.localPosition += new Vector3(-70 * num, 0, 0);
            num++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            StartCoroutine(LoadtoBattle());
            start= false;
        }
        
        GF.SetTextMeshPro(TowerHP, $"{hp}");
        GF.SetTextMeshPro(EnemyNumber, $"{enemyNumber}/{passedEnemyNumber}");
        GF.SetTextMeshPro(Cost, $"{costValue}");
        GF.SetTextMeshPro(AbletoSetting, $"{AbletoValue}");
        // { 체력이 남아있는 상황
        if (hp >0)
        {       
            if (passedEnemyNumber == enemyNumber)
            {
                Time.timeScale = 0;
                missionabnner.SetActive(true);
                missionAccomplished.SetActive(true);
                if (missionAccomplished.transform.localPosition.x < 0)
                {
                    missionAccomplished.transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime, Space.Self);
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                    if (timer > 1)
                    {
                        if (missionAccomplished.transform.localPosition.x < 1300)
                        {
                            missionAccomplished.transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime, Space.Self);
                        }
                        else
                        {
                            missionabnner.SetActive(false);
                            missionAccomplished.SetActive(false);
                            resultBanner.SetActive(true);
                            
                        }
                    }
                }
            }
        }
        // } 체력이 남아있는 상황
        // { 체력이 0인 상황
        else
        {
            Time.timeScale = 0;
            missionFailed.SetActive(click);
            if(Input.GetMouseButtonUp(0))
            {
                click= false;
            }
        }
        // } 체력이 0인 상황
        // { 결과창 출력
        //if()
        // } 결과창 출력
        // { 코스트 1초마다 1씩 회복, 최대 99
        if (costValue < 99)
        {
            time += Time.deltaTime;
            CostGuage.fillAmount = time / 1f;
            if (time >= 1)
            {
                time = 0f;
                costValue++;
            }
        }
        // } 코스트 1초마다 1씩 회복, 최대 99
    }
    public void SettingImage()
    {
        if (allSet == false)
        {
            operatorImgSet.Clear();
            operatorImgSet.AddRange(operatorImg);

        }
    }
    public void OnPause()
    {
        if (onPause)
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
        if (onPause == false)
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
    public void ResultClick()
    {
        StartCoroutine(BacktoStage());
    }
    public IEnumerator LoadtoBattle()
    {
        Time.timeScale = 0;
        Color c = FadePannel.GetComponent<Image>().color;
        c.a = 1;
        FadePannel.GetComponent<Image>().color = c;
        yield return new WaitForSecondsRealtime(0.5f);
        for (float f = 1f; f > 0; f -= 0.002f)
        {
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        
        FadePannel.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
    }
    public IEnumerator BacktoStage()
    {
        SceneManager.LoadScene(GDate.SCENE_NAME_STAGE);
        yield return null;
    }
}
