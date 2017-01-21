using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public GameObject player = null;

    private GameObject map;
    private float topLimit;
    private float botLimit;
    private float leftLimit;
    private float rightLimit;

    private float cameraVertical;
    private float cameraHorizont;

	// Use this for initialization
    void Start()
    {
        cameraVertical = Camera.main.orthographicSize;
        cameraHorizont = cameraVertical * Screen.width / Screen.height;

        if (player == null)
            player = GameObject.FindGameObjectWithTag(KeyCodes.PlayerWarrior);
        if (map == null)
            map = GameObject.FindGameObjectWithTag(KeyCodes.Map);


    }
	
	// Update is called once per frame
    void Update()
    {
        //if(camera.transform.position.x )
        //camera.orthographicSize

        
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}


}
