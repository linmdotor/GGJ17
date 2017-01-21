﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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

    // Use this for initialization
	void Start () {

        score = 0;
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
	}
	
	// Update is called once per frame
	void Update () {

        if (levelIsReady)
        {
    	    if(numberOfEnemiesLeft == 0)
                Debug.Log("Level won");
        }
	}

    public void increaseScore(float score)
    {
        this.score += score;
        UIManager.UIManagerInstance.changeScore(this.score);
    }

    public void gameOver()
    {
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
}
