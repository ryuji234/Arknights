using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitileUI : MonoBehaviour
{
    public void Combat()
    {
        SceneManager.LoadScene(GDate.SCENE_NAME_MAIN);
    }
}
