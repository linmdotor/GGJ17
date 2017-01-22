using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistentScore : MonoBehaviour {

    #region Singleton
    public static PersistentScore PersistentScoreInstance;

    void Awake()
    {
        if (PersistentScoreInstance == null)
            PersistentScoreInstance = gameObject.GetComponent<PersistentScore>();
    }
    #endregion

    public float finalScore;
    
    [HideInInspector]
    public List<Score> scores;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
	}

    public void setFinalScore(float score,string name)
    {
        Load();
        finalScore = score;
        
        Score newScore = new Score();
        newScore.score = score;
        newScore.name = name;

        if (scores == null)
        {
            scores = new List<Score>();
            Debug.Log("score vacio");
        }

        scores.Add(newScore);
        scores.Sort(CompareScoresByScore);
        Save();
    }

    private static int CompareScoresByScore(Score score1, Score score2)
    {
        return score2.score.CompareTo(score1.score);
    }

    private void drawScores()
    {
        foreach(Score currentScore in scores)
        {
            Debug.Log(currentScore.name + " " + currentScore.score);

        }
    }
    public float getFinalScore(float score)
    {
        return finalScore;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerScores data = new PlayerScores();
              
        data.scores = scores;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        //Debug.Log("adsf");

        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            //Debug.Log("file exist");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
            PlayerScores data = (PlayerScores)bf.Deserialize(file);
            //print(Application.persistentDataPath);

            scores = data.scores;

            file.Close();

        }
    }

    public void ResetScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerScores data = new PlayerScores();

        scores = new List<Score>();

        data.scores = scores;

        bf.Serialize(file, data);
        file.Close();
    }


    
}

[System.Serializable]
public class PlayerScores
{
    public List<Score> scores;
}

[System.Serializable]
public class Score
{
    public float score;
    public string name;
}