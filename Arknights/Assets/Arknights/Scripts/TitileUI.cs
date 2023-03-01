using UnityEngine;
using UnityEngine.SceneManagement;

public class TitileUI : MonoBehaviour
{


    private void Start()
    {

    }
    public void Combat()
    {
        SoundManager.Click();
        SceneManager.LoadScene(GDate.SCENE_NAME_MAIN);
    }
}
