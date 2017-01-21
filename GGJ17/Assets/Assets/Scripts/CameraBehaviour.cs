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
        MapTile topLeft = MapManager.MapManagerInstance.GetMapTile(0, 0);
        MapTile botRight = MapManager.MapManagerInstance.GetMapTile(MapManager.MapManagerInstance.mapSize_X - 1, MapManager.MapManagerInstance.mapSize_Y - 1);

        topLimit = (topLeft.transform.position.y + MapTile.TileLength / 2) - Camera.main.orthographicSize;
        botLimit = (botRight.transform.position.y - MapTile.TileLength / 2) + Camera.main.orthographicSize;
        leftLimit = (topLeft.transform.position.x - MapTile.TileLength / 2) + Camera.main.orthographicSize * Screen.width / Screen.height;
        rightLimit = (botRight.transform.position.x + MapTile.TileLength / 2) - Camera.main.orthographicSize * Screen.width / Screen.height;

        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, botLimit, topLimit);
        transform.position = newPosition;
	}


}
