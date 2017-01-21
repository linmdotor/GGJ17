using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int life;

    private bool hit;
    private bool onDamageZone;
    private float invTimeBase = 3;
    private float invTimeBaseActual;

	// Use this for initialization
	void Start () {
        invTimeBaseActual = invTimeBase;
	}
	
	// Update is called once per frame
	void Update () {
        if (onDamageZone)
        {
            invTimeBaseActual -= Time.deltaTime;
        }
	    if (life == 0)
        {
            death();
        }
	}

    void OnTriggerStay2D(Collider2D wave)
    {
        if (wave.tag == KeyCodes.Wave && !hit)
        {
            damage();
            hit = true;
            onDamageZone = true;
        }
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
