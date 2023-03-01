using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUI : MonoBehaviour
{
    public void Stage1_11()
    {
        SoundManager.BattleStart();
        SceneManager.LoadScene(GDate.SCENE_NAME_BATTLE);
    }
    public void BackToMain()
    {
        SoundManager.Click();
        SceneManager.LoadScene(GDate.SCENE_NAME_MAIN);
    }

    public void BackToTitle()
    {
        SoundManager.Click();
        SceneManager.LoadScene(GDate.SCENE_NAME_TITLE);
    }

}
