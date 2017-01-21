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
        UIManager.UIManagerInstance.changeLifeText(life);
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
            damage();
            hit = true;
            onDamageZone = true;
        }
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
        UIManager.UIManagerInstance.changeLifeText(life);
    }
    public void attackEnded()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Attack", false);
        this.transform.GetChild(0).GetComponent<AttackManager>().animationEnded();
    }
}
