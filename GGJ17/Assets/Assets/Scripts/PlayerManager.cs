using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int life;

    [SerializeField]
    private bool hit;
    [SerializeField]
    private bool onDamageZone;
    [SerializeField]
    private float invTimeBase = 3;
    [SerializeField]
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
            if (invTimeBaseActual <= 0)
            {
                damage();
                invTimeBaseActual = invTimeBase;
            }
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
            print("HOLO");
            damage();
            hit = true;
            onDamageZone = true;
        }
        //EL PLAYER CONTROLA CADA X TIEMPO SI ESTÁ EN UNA ONDA, EN ESE CASO SE QUITA VIDA
    }

    void OnTriggerExit2D(Collider2D wave)
    {
        if (wave.tag == KeyCodes.Wave)
        {
            hit = false;
            onDamageZone = false;
            invTimeBaseActual = invTimeBase;
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
