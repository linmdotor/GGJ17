using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    private GameObject player = null;

	// Use this for initialization
	void Start() {
        if (player == null)
            player = GameObject.FindGameObjectWithTag(KeyCodes.Player);
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
	
	// Update is called once per frame
    void Update(){
        print("HOLO");
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}


}
