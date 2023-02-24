using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ScrollingUI : MonoBehaviour
{
    public List<GameObject> FoundObjects;
    public List<Sprite> Sprites;
    public GameObject middle;
    public GameObject stage;
    public GameObject stage2;
    public GameObject Bg;
    public GameObject FadePannel;
    public GameObject FadePannel2;
    public GameObject episodeName;
    public RectTransform Content;
    public float shortDis;
    private bool click = true;
    private bool changeSprite = true;
    private bool selectEpisode = default;
    private void Start()
    {
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("UI"));
        shortDis = Mathf.Infinity;
        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(middle.transform.position, found.transform.Find("Stage").gameObject.transform.position);
            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                stage = found;
                shortDis = Distance;
            }
        }
        StartCoroutine(BackGround());
    }
    void Update()
    {

        Scrolling();

        shortDis = Mathf.Infinity;
        foreach (GameObject found in FoundObjects)
        {
            
            float Distance = Vector3.Distance(middle.transform.position, found.transform.Find("Stage").gameObject.transform.position);
            // { 안 고쳐지면 빼버릴 거임
            //if (Distance < 30)
            //{
                
            //    found.transform.localScale = Vector3.Lerp(new Vector3(1f,1f,1f),new Vector3(1.2f,1.2f,1f), Distance);
            //}
            //else
            //{
            //    found.transform.localScale = new Vector3(1, 1, 1);
            //}
            // } 안 고쳐지면 빼버릴 거임
            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                stage = found;
                shortDis = Distance;
            }
        }
        Debug.Log(shortDis);
        if (Input.GetMouseButtonDown(0))
        {
            click = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            click = true;
            changeSprite = true;
        }
    }

    public void Scrolling()
    {
        //Debug.Log(stage.name);
        switch (stage.name)
        {
            case "Stage_1":
                GF.SetTextMeshPro(episodeName, "암흑 시대.상");
                break;
            case "Stage_2":
                GF.SetTextMeshPro(episodeName, "암흑 시대.하");
                break;
            case "Stage_3":
                GF.SetTextMeshPro(episodeName, "같지만 다른");
                break;
            case "Stage_4":
                GF.SetTextMeshPro(episodeName, "세컨드 윈드");
                break;

        }
        if (click && changeSprite)
        {
            switch (stage.name)
            {
                case "Stage_1":
                    Content.anchoredPosition = new Vector3(310.0f, 0.0f, 0.0f);
                    Bg.GetComponent<Image>().sprite = Sprites[0];

                    break;
                case "Stage_2":
                    Content.anchoredPosition = new Vector3(110.0f, 0.0f, 0.0f);
                    Bg.GetComponent<Image>().sprite = Sprites[1];

                    break;
                case "Stage_3":
                    Content.anchoredPosition = new Vector3(-110.0f, 0.0f, 0.0f);
                    Bg.GetComponent<Image>().sprite = Sprites[2];

                    break;
                case "Stage_4":
                    Content.anchoredPosition = new Vector3(-310.0f, 0.0f, 0.0f);
                    Bg.GetComponent<Image>().sprite = Sprites[3];

                    break;

            }
            changeSprite = false;


            if (selectEpisode)
            {
                StartCoroutine(SelectEpisode());
            }

            if (stage != stage2)
            {
                StopAllCoroutines();
                StartCoroutine(FadeInStart());
                StartCoroutine(BackGround());
            }

            stage2 = stage;
            //Debug.Log("실행");

        }
    }

    IEnumerator BackGround()
    {
        float decrease = 0.99f;
        Bg.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        while (Bg.transform.localScale.x > 1.2)
        {

            yield return new WaitForSeconds(0.05f);
            Bg.transform.localScale *= decrease;
            //decrease *= 0.9f;
        }
        if (Bg.transform.localScale.x < 1.2f)
        {
            Bg.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
        }
    }
    public IEnumerator FadeInStart()
    {

        FadePannel.SetActive(true);
        Color c = FadePannel.GetComponent<Image>().color;
        c.a = 1;
        FadePannel.GetComponent<Image>().color = c;
        yield return new WaitForSeconds(0.5f);
        for (float f = 1f; f > 0; f -= 0.002f)
        {
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        FadePannel.SetActive(false);

    }

    public IEnumerator SelectEpisode()
    {
        yield return new WaitForSeconds(1.5f);
        switch (stage.name)
        {
            case "Stage_1":

                break;
            case "Stage_2":
                SceneManager.LoadScene(GDate.SCENE_NAME_STAGE);
                break;
            case "Stage_3":

                break;
            case "Stage_4":

                break;

        }

        selectEpisode = false;
    }

    public void Stage1btn()
    {
        if (stage.name != "Stage_1")
        {
            stage = FoundObjects[0];
            Content.anchoredPosition = new Vector3(310.0f, 0.0f, 0.0f);
            Bg.GetComponent<Image>().sprite = Sprites[0];
            Bg.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        selectEpisode = true;

    }
    public void Stage2btn()
    {
        if (stage.name != "Stage_2")
        {
            stage = FoundObjects[1];
            Content.anchoredPosition = new Vector3(110.0f, 0.0f, 0.0f);
            Bg.GetComponent<Image>().sprite = Sprites[1];
            Bg.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        selectEpisode = true;


    }
    public void Stage3btn()
    {
        if (stage.name != "Stage_3")
        {
            stage = FoundObjects[2];
            Content.anchoredPosition = new Vector3(-110.0f, 0.0f, 0.0f);
            Bg.GetComponent<Image>().sprite = Sprites[2];
            Bg.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        selectEpisode = true;

    }
    public void Stage4btn()
    {
        if (stage.name != "Stage_4")
        {
            stage = FoundObjects[3];
            Content.anchoredPosition = new Vector3(-310.0f, 0.0f, 0.0f);
            Bg.GetComponent<Image>().sprite = Sprites[3];
            Bg.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        selectEpisode = true;

    }
}
