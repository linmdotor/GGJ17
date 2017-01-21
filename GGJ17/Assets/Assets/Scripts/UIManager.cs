using UnityEngine;
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
       
    void Start()
    {
        enemyText = transform.Find("EnemiesText").GetComponent<Text>();
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
    }
    public void changeNumberOfEnemies(int enemiesNumber)
    {
        enemyText.text = "Enemies: "+enemiesNumber;
    }

    public void changeScore(float score)
    {
        scoreText.text = "Score: " + score;
    }
}
