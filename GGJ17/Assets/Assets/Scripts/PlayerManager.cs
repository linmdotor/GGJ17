﻿using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int life;

    private bool hit;
    private bool onDamageZone;
    private float invTimeBase = 3;
    private float invTimeBaseActual;
    private bool damageFeedbackActive = false;
    private bool foilFeedbackActive = false;

    public bool foilOn = false;
    public float foilTime = 0;

	// Use this for initialization
	void Start () {
        invTimeBaseActual = invTimeBase;
        UIManager.UIManagerInstance.changeLifeText(life);
        this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getPlayerDamage();
    }
	
	// Update is called once per frame
	void Update () {
        if (foilTime > 0)
        {
            if (!foilFeedbackActive)
                StartCoroutine(foilFeedback());
            foilTime -= Time.deltaTime;
            if(foilTime <= 0)
            {
                foilOn = false;
                foilTime = 0;
            }
            
        }
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

    void OnTriggerEnter2D(Collider2D powerUp)
    {
        if(powerUp.tag == KeyCodes.PowerUp)
        {
            powerUp.transform.gameObject.GetComponent<PowerUp>().effect();
            powerUp.transform.gameObject.GetComponent<PowerUp>().deletePowerUp();
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
        this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getPlayerDeath();
        this.GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag(KeyCodes.GameManager).GetComponent<GameManager>().gameOver();
    }
    public void damage()
    {
        this.GetComponent<AudioSource>().Play();
        if (!foilOn) 
        { 
            --life;
            UIManager.UIManagerInstance.changeLifeText(life);

            if (!damageFeedbackActive)
                StartCoroutine(damageFeedback());
        }
    }
    public void attackEnded()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Attack", false);
        this.transform.GetChild(0).GetComponent<AttackManager>().animationEnded();
    }

    IEnumerator damageFeedback()
    {
        damageFeedbackActive = true;
        float waitTime = 0.1f;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.white;
        damageFeedbackActive = false;
    }
    IEnumerator foilFeedback()
    {
        foilFeedbackActive = true;
        float waitTime = 0.2f;
        GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.magenta;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.magenta;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = Color.white;
        foilFeedbackActive = false;
    }
}
