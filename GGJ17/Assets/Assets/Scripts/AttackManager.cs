using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour {

    private bool enemyHit;
    private float timeToDisable;

    void OnEnable()
    {
        this.transform.localPosition = new Vector3(1, 0.5f, 0);
        enemyHit = false;
        timeToDisable = 0.8f;
    }

	// Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(1, 0.5f, 0);
        timeToDisable -= Time.deltaTime;
        if (timeToDisable <= 0)
        {
            this.gameObject.transform.parent.GetComponent<Animator>().Play("Idle");
            animationEnded();
        }
	}

    void OnTriggerStay2D(Collider2D enemy)
    {
        if(enemy.tag == KeyCodes.Enemy && enemyHit == false)
        {
            enemyHit = true;
            enemy.gameObject.GetComponent<Enemy>().damage();
        }
    }
    public void animationEnded()
    {
        this.gameObject.SetActive(false);
    }
}
