using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int life = 1;
    public float score = 10;
    public GameObject crashAnimationPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (life == 0)
            death();
	}

    private void death()
    {
        GameManager.GameManagerInstance.increaseScore(score);
        GameManager.GameManagerInstance.removeEnemy();

        if(crashAnimationPrefab != null)
        {
            GameObject crash = (GameObject)Instantiate(crashAnimationPrefab, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
    public void damage()
    {
        --life;
    }
}
