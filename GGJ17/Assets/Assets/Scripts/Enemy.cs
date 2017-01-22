using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


    private bool destroy = false;
    public int life = 1;
    public float score = 10;
    public GameObject crashAnimationPrefab;

    private enum EnemyType { thing, guy };

    [SerializeField]
    private EnemyType enemyType;

    public bool alreadyDead = false;
    

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
        if (!alreadyDead)
        {
            if (destroy)
            {
                death();
            }
            if (life == 0)
            {
                foreach (Transform child in this.transform)
                {
                    if(child.name != "NPCSprite")
                        child.localScale = new Vector3(0, 0, 0);

					if(GetComponent<EnemyMovement>())
						GetComponent<EnemyMovement>().enabled = false;
                    destroy = true;
                }
            }
        }
        
	}

    private void death()
    {
        alreadyDead = true;
        GameManager.GameManagerInstance.increaseScore(score);
		if(gameObject.transform.Find("NPCSprite"))
			gameObject.transform.Find("NPCSprite").GetComponent<Animator>().SetTrigger("Dead");
        GameManager.GameManagerInstance.removeEnemy();

        //gameObject.transform.Find

        if(crashAnimationPrefab != null)
        {
            GameObject crash = (GameObject)Instantiate(crashAnimationPrefab, this.transform.position, Quaternion.identity);
        }
        if(enemyType == EnemyType.thing)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Destroy Waves
            Destroy(GetComponentInChildren<WaveEffect>().gameObject);
            Destroy(this.gameObject, 1.5f);
        }
    }

    public void damage()
    {
        --life;
    }
}
