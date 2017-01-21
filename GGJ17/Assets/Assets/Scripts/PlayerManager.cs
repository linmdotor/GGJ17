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

    void OnTriggerStay2D(Collider2D wave)
    { 
        //EL PLAYER CONTROLA CADA X TIEMPO SI ESTÁ EN UNA ONDA, EN ESE CASO SE QUITA VIDA
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
