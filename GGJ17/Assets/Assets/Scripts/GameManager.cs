using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Texture2D newCursor;
    public float score;

    #region Singleton
    public static GameManager GameManagerInstance;
    
    void Awake()
    {
        if (GameManagerInstance == null)
            GameManagerInstance = gameObject.GetComponent<GameManager>();
    }
    #endregion

    public int numberOfEnemiesLeft = 0;
    public bool levelIsReady = false;
    public int actualLevel = 1;


    // Use this for initialization
	void Start () {

        Cursor.SetCursor(newCursor, new Vector2(64f, 64f), CursorMode.Auto);


        score = 0;
        actualLevel = 1;
        //GUIManager.GUIManagerInstance.setInitialValues(score, life);

        #region HighScore persistente
        //PersistentScore.PersistentScoreInstance.Load();
        //PersistentScore.PersistentScoreInstance.ResetScores();
        //GUIManager.GUIManagerInstance.updateHighScore(PersistentScore.PersistentScoreInstance.scores);
        #endregion

        //pauseMenuContainer = GameObject.FindGameObjectWithTag(Tags.PauseMenu);
        //pauseMenu = pauseMenuContainer.transform.Find("PauseMenu").gameObject;

        this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getSoundTrack();
        this.GetComponent<AudioSource>().Play();

        PersistentScore.PersistentScoreInstance.Load();

	}
	
	// Update is called once per frame
	void Update () {

    	    if(numberOfEnemiesLeft <= 0)
            {
                actualLevel++;
                UIManager.UIManagerInstance.winMenu.SetActive(true);
            }

	}

    public void increaseScore(float score)
    {
        this.score += score;
        UIManager.UIManagerInstance.changeScore(this.score);
    }

    public void gameOver()
    {
        //UIManager.UIManagerInstance.deadMenu.SetActive(true);
        UIManager.UIManagerInstance.playerDead();
        //Load gameOver screen
        //Update scores
        //Deactivate everything (?)
    }

    public void removeEnemy()
    {
        numberOfEnemiesLeft--;
        UIManager.UIManagerInstance.changeNumberOfEnemies(numberOfEnemiesLeft);
    }

    public void addEnemy()
    {
        numberOfEnemiesLeft++;
        UIManager.UIManagerInstance.changeNumberOfEnemies(numberOfEnemiesLeft);
    }

    public void cleanEnemies()
    {
        numberOfEnemiesLeft = 0;
        UIManager.UIManagerInstance.changeNumberOfEnemies(numberOfEnemiesLeft);
    }
}
