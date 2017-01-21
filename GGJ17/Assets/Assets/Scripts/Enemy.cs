using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int life = 1;
    public float score = 10;

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
        GameObject.FindGameObjectWithTag(KeyCodes.GameManager).GetComponent<GameManager>().increaseScore(score);
        Destroy(this.gameObject);
    }
    public void damage()
    {
        --life;
    }
}
