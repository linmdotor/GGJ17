using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class DeadCanvas : MonoBehaviour {

    #region Singleton
    public static DeadCanvas DeadCanvasInstance;

    void Awake()
    {
        if (DeadCanvasInstance == null)
            DeadCanvasInstance = gameObject.GetComponent<DeadCanvas>();
    }
    #endregion


    [Header("HighScore")]
    public Text yourScoreNumber; 
    public Text firstScoreNumber;
    public Text secondScoreNumber;
    public Text thirdScoreNumber;
    public Text fourthScoreNumber;
    public Text fifthScoreNumber;


	// Use this for initialization
	void Start () {
        yourScoreNumber.text = ""+ GameManager.GameManagerInstance.score;
        updateHighScore(PersistentScore.PersistentScoreInstance.scores);
      
	}
	

    public void updateHighScore(List<Score> scores)
    {
        int count = scores.Count;
        firstScoreNumber.text = "";
        secondScoreNumber.text = "";
        thirdScoreNumber.text = "";
        fourthScoreNumber.text = "";
        fifthScoreNumber.text = "";

        if (count > 0)
            firstScoreNumber.text = "" + scores[0].name + " " + scores[0].score;
        if (count > 1)
            secondScoreNumber.text = "" + scores[1].name + " " + scores[1].score;
        if (count > 2)
            thirdScoreNumber.text = "" + scores[2].name + " " + scores[2].score;
        if (count > 3)
            fourthScoreNumber.text = "" + scores[3].name + " " + scores[3].score;
        if (count > 4)
            fifthScoreNumber.text = "" + scores[4].name + " " + scores[4].score;

    }

 

    public void playAgainButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenuButtonPressed()
    {
        SceneManager.LoadScene("StartScene");
    }
}
