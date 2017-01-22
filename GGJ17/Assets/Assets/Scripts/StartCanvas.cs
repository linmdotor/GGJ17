using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartCanvas : MonoBehaviour {


    public void playButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void controlsButton()
    {
        SceneManager.LoadScene("CONTROLS");
    }

    public void creditsButton()
    {
        SceneManager.LoadScene("CREDITS");
    }

    public void exitButton()
    {
        Application.Quit();
    }
}
