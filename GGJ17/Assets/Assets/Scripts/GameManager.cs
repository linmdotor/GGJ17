using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public int score;

    #region Singleton
    public static GameManager GameManagerInstance;

    void Awake()
    {
        if (GameManagerInstance == null)
            GameManagerInstance = gameObject.GetComponent<GameManager>();
    }
    #endregion
	
    // Use this for initialization
	void Start () {

        score = 0;
        //GUIManager.GUIManagerInstance.setInitialValues(score, life);

        #region HighScore persistente
        //PersistentScore.PersistentScoreInstance.Load();
        //PersistentScore.PersistentScoreInstance.ResetScores();
        //GUIManager.GUIManagerInstance.updateHighScore(PersistentScore.PersistentScoreInstance.scores);
        #endregion
        #region Audio
        //this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getSoundTrack();
        //this.GetComponent<AudioSource>().Play();
        #endregion

        //pauseMenuContainer = GameObject.FindGameObjectWithTag(Tags.PauseMenu);
        //pauseMenu = pauseMenuContainer.transform.Find("PauseMenu").gameObject;

        if (player == null)
            player = GameObject.FindGameObjectWithTag(KeyCodes.Player);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void increaseScore(int score)
    {
        this.score += score;
    }
}
