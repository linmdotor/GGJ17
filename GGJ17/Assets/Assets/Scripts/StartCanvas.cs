using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartCanvas : MonoBehaviour {


    public void playButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void exitButton()
    {
        Application.Quit();
    }
}
