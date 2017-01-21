using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour {

    private bool enemyHit;

    void OnEnable()
    {
        enemyHit = false;
    }

	// Update is called once per frame
	void Update () {
	    
	}

    void onTriggerEnter2D(Collider enemy)
    {
        if(enemy.tag == KeyCodes.Enemy && enemyHit == false)
        {
            enemyHit = true;
            enemy.gameObject.GetComponent<EnemyManager>().damage();
        }
    }
    public void animationEnded()
    {
        this.gameObject.SetActive(false);
    }
}
