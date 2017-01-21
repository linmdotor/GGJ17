using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {


    #region Singleton
    public static UIManager UIManagerInstance;

    void Awake()
    {
        if (UIManagerInstance == null)
            UIManagerInstance = gameObject.GetComponent<UIManager>();
    }
    #endregion



    Text enemyText;
    Text scoreText;
    GameObject pauseMenu;

    private bool paused = false;

    void Start()
    {
        enemyText = transform.Find("EnemiesText").GetComponent<Text>();
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
        pauseMenu = transform.Find("PauseMenu").gameObject;
        
    }

    void Update()
    {
        if (Input.GetButton(KeyCodes.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

    }
    public void changeNumberOfEnemies(int enemiesNumber)
    {
        enemyText.text = "Enemies: "+enemiesNumber;
    }

    public void changeScore(float score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ResumeButton()
    {
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ExitButton()
    {
        paused = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}
