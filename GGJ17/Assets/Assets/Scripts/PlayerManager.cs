using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int life;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(life == 0)
        {
            death();
        }
	}

    private void death()
    {
        GameObject.FindGameObjectWithTag(KeyCodes.GameManager).GetComponent<GameManager>().gameOver();
    }
    public void damage()
    {
        --life;
    }
}
