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
    public static bool isDead = false;
    public static bool isEscape = false;
    public static bool active = false;
    public static int hp = 5;
    public static int passedEnemyNumber;
    public static int costValue;
    public static int AbletoValue;
    public static bool onDoubleSpeed = false;
    public List<Image> operatorImg = new List<Image>();
    public List<AudioClip> audioClips = new List<AudioClip>();
    public static AudioSource uiAudio;

    private List<Image> operatorImgSet = new List<Image>();
    private bool onPause = false;
    private bool start = true;
    private int num = 0;
    private int enemyNumber = 11;
    private float time = 0;
    private int speed = 30;
    private float timer = 0;
    private bool click;
    private bool battleFinish = false;
    void Start()
    {
        battleFinish = false;
        onDoubleSpeed = false;
        onPause= false;
        uiAudio = GetComponent<AudioSource>();
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
            operatorImgSet.Add(Instantiate(setting, OperatorSetting.transform));
        }

        for (int i = 0; i < operatorImgSet.Count; i++)
        {
            operatorImgSet[i].transform.localPosition = new Vector3(-70f * i, 0, 0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            StartCoroutine(LoadtoBattle());
            start = false;
        }

        if (active)
        {
            for (int i = 0; i < operatorImgSet.Count; i++)
            {
                if (operatorImgSet[i].gameObject.activeSelf)
                {
                    operatorImgSet[i].transform.localPosition = new Vector3(-70f * num, 0, 0);
                    num++;
                }

            }
            num = 0;
            active = false;
        }

        GF.SetTextMeshPro(TowerHP, $"{hp}");
        GF.SetTextMeshPro(EnemyNumber, $"{enemyNumber}/{passedEnemyNumber}");
        GF.SetTextMeshPro(Cost, $"{costValue}");
        GF.SetTextMeshPro(AbletoSetting, $"{AbletoValue}");
        // { 체력이 남아있는 상황
        if (hp > 0)
        {
            // { 미션 성공
            if (passedEnemyNumber == enemyNumber)
            {
                Time.timeScale = 0;
                missionabnner.SetActive(true);
                missionAccomplished.SetActive(true);
                if(!battleFinish)
                {
                    uiAudio.clip = audioClips[1];
                    uiAudio.Play();
                    battleFinish = true;
                }
                
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
            // } 미션 성공
        }
        // } 체력이 남아있는 상황
        // { 체력이 0인 상황
        else
        {
            Time.timeScale = 0;
            if(!battleFinish)
            {
                uiAudio.clip = audioClips[0];
                uiAudio.Play();
                battleFinish = true;
            }
            missionFailed.SetActive(click);
            if (Input.GetMouseButtonUp(0))
            {
                click = false;
                resultBanner.SetActive(true);
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
        if(isDead)
        {
            Dead();
            isDead= false;
        }
        if (isEscape)
        {
            Escape();
            isEscape= false;
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
    public void Dead()
    {
        uiAudio.PlayOneShot(audioClips[2]);
    }
    public void Escape()
    {
        uiAudio.PlayOneShot(audioClips[3]);
    }

    public IEnumerator LoadtoBattle()
    {
        Time.timeScale = 0;
        Color c = FadePannel.GetComponent<Image>().color;
        c.a = 1;
        FadePannel.GetComponent<Image>().color = c;
        yield return new WaitForSecondsRealtime(0.5f);
        for (float f = 1f; f > 0; f -= 0.02f)
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
        Time.timeScale = 1;
        SceneManager.LoadScene(GDate.SCENE_NAME_STAGE);
        yield return null;
    }
    //public IEnumerator Win()
    //{
    //    Time.timeScale = 0;
    //    missionabnner.SetActive(true);
    //    missionAccomplished.SetActive(true);
    //    uiAudio.clip = audioClips[1];
    //    uiAudio.Play();

    //    while (missionAccomplished.transform.localPosition.x < 0)
    //    {
    //        missionAccomplished.transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime, Space.Self);
    //    }


    //    yield return new WaitForSecondsRealtime(1f);

    //    while (missionAccomplished.transform.localPosition.x < 1300)
    //    {
    //        missionAccomplished.transform.Translate(Vector3.right * speed * Time.unscaledDeltaTime, Space.Self);
    //    }

    //    missionabnner.SetActive(false);
    //    missionAccomplished.SetActive(false);
    //    resultBanner.SetActive(true);

    //}
}
