using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void onTriggerEnter2D(Collider enemy)
    {
        if(enemy.tag == KeyCodes.Enemy)
        {
            //NOMBRE MANAGER ENEMIGO
            //enemy.gameObject.GetComponent<>().damage();
        }
    }
}
