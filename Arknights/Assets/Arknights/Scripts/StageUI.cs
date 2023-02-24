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

}
