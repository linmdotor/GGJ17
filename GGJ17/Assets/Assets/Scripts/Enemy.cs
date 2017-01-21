﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


    private bool destroy = false;
    public int life = 1;
    public float score = 10;
    public GameObject crashAnimationPrefab;

    private enum EnemyType { thing, guy };

    [SerializeField]
    private EnemyType enemyType;
    

	// Use this for initialization
	void Start () {
        if (enemyType == EnemyType.thing)
        {
            this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getEmisorMusic();
            this.GetComponent<AudioSource>().Play();
        }
        else if(enemyType == EnemyType.guy)
        {
            this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getEnemyMusic();
            this.GetComponent<AudioSource>().Play();
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            death();
        }
        if (life == 0)
        {
            foreach (Transform child in this.transform)
            {
                child.localScale = new Vector3(0, 0, 0);
                destroy = true;
            }
        }
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
