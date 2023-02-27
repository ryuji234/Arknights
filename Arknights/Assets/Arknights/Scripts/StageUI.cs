using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUI : MonoBehaviour
{
    public void Stage1_11()
    {
        SceneManager.LoadScene(GDate.SCENE_NAME_BATTLE);
    }
    public void BackToMain()
    {
        SceneManager.LoadScene(GDate.SCENE_NAME_MAIN);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(GDate.SCENE_NAME_TITLE);
    }

}
