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
    Text lifeText;
	Image lifeImage;
	Image lifeBar;
	Text deadMenuScoreText, winMenuScoreText;
    [HideInInspector]
    public GameObject pauseMenu, deadMenu, winMenu;

    private bool paused = false;

    public GameObject nameSelector;
    public GameObject buttons;

    public GameObject playAgainButton;


    void Start()
    {
        enemyText = transform.Find("UIGame/EnemiesText").GetComponent<Text>();
        scoreText = transform.Find("UIGame/ScoreText").GetComponent<Text>();
        lifeText = transform.Find("UIGame/LifeText").GetComponent<Text>();
        lifeImage = transform.Find("UIGame/LifeText").Find("LifeImage").GetComponent<Image>();
		lifeImage.sprite = enemyLifeImages[0];
        lifeBar = transform.Find("UIGame/LifeText").Find("LifeBar").GetComponent<Image>();
		pauseMenu = transform.Find("PauseMenu").gameObject;

        deadMenu = transform.Find("DeadMenu").gameObject;
        deadMenuScoreText = deadMenu.transform.Find("ScoreText").GetComponent<Text>();

        winMenu = transform.Find("WinMenu").gameObject;
        winMenuScoreText = winMenu.transform.Find("ScoreText").GetComponent<Text>();

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
        enemyText.text = "<b>" + enemiesNumber + "</b>";
    }

    public void changeScore(float score)
    {
        scoreText.text = "<b>Score: " + score + "</b>";
        deadMenuScoreText.text = "<b>Score: " + score + "</b>";
        winMenuScoreText.text = "<b>Score: " + score + "</b>";
    }



	public Sprite[] enemyLifeImages;
	private int[,] lifeLimits = new int[4,2] { { 0, 25 }, 
											{ 26, 50 }, 
											{ 51, 75 }, 
											{ 76, 100 } };
	public void changeLifeText(int life)
    {
		//TEXT
        //lifeText.text = "Life: " + life;

		//LIFE BAR
		lifeBar.fillAmount = life / 100.0f;

		//IMAGE
		//Busca en qué nivel de vida está, y cambia el sprite según este
		for (int i = 0; i<4; ++i)
		{
			if(life >= lifeLimits[i,0] && life <= lifeLimits[i,1]) //si está en el rango actual
			{
				lifeImage.sprite = enemyLifeImages[i];
			}
		}

    }

    public void ResumeButton()
    {
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ReplayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void ChargeLevel()
    {
        Time.timeScale = 1f;
        //ToDo hacer que se cargue el mapa teniendo en cuenta que nivel es
        //SceneManager.LoadScene("MainScene");
        Destroy(GameObject.FindGameObjectWithTag(KeyCodes.PlayerWarrior));
        MapManager.MapManagerInstance.GenerateLevel(GameManager.GameManagerInstance.actualLevel);
        winMenu.SetActive(false);


    }

    public void ExitButton()
    {
        paused = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }


    public void playerDead()
    {
        transform.Find("DeadCanvas").gameObject.SetActive(true);
        transform.Find("UIGame").gameObject.SetActive(false);

        

    }
}
