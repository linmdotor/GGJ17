using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

    public GameObject powerUpPrefab;

    [HideInInspector]
    public float posX;
    [HideInInspector]
    public float posY;

    public float spawnCheckTime = 5;
    public int spawnProbability = 10;
    private int timesSinceLastSpawn = 0;

    public float timeToCheck;
    
    public enum powerUps
    {
        Foil,
        Sandwich,
        RedBull
    };

	// Use this for initialization
	void Start () {
        timeToCheck = spawnCheckTime;
	}
	
	// Update is called once per frame
	void Update () {
        timeToCheck -= Time.deltaTime;

        if (timeToCheck <= 0)
        {
            int check = Random.Range(0, 100);

            if (check < spawnProbability)
                spawnItem();
            else
                timesSinceLastSpawn++;

            timeToCheck = spawnCheckTime;
        }

        if (timesSinceLastSpawn == 5)
            spawnItem();
	}

    public void spawnItem()
    {
        MapTile[] spawnPlace = MapManager.MapManagerInstance.GetMapTiles(MapTile.TileType.Floor);
        MapTile freeTile = spawnPlace[Random.Range(0, spawnPlace.Length)];
        posX = freeTile.transform.position.x;
        posY = freeTile.transform.position.y;
        GameObject powerUp = (GameObject)GameObject.Instantiate(powerUpPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
        powerUp.GetComponent<PowerUp>().tileXCord = freeTile.logicPosition_X;
        powerUp.GetComponent<PowerUp>().tileYCord = freeTile.logicPosition_Y;
        timesSinceLastSpawn = 0;
    }
}
