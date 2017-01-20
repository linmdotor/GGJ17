using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int life;
    public 

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

    public void death()
    {
        //gameManager -> game over
    }
    public void damage()
    {
        --life;
    }
}
