using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

    public GameObject powerUpPrefab;
    public float posX;
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
        MapTile spawnPlace = null;
        //POR IMPLEMENTAR
        //MapTile spawnPlace = MapManager.MapManagerInstance.getFreeTiles();
        posX = spawnPlace.transform.position.x;
        posY = spawnPlace.transform.position.y;
        GameObject powerUp = (GameObject)GameObject.Instantiate(powerUpPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
        powerUp.GetComponent<PowerUp>().tileXCord = spawnPlace.logicPosition_X;
        powerUp.GetComponent<PowerUp>().tileYCord = spawnPlace.logicPosition_Y;
        timesSinceLastSpawn = 0;
    }
}
