using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    #region Singleton
    public static EnemyManager EnemyManagerInstance;

    void Awake()
    {
        if (EnemyManagerInstance == null)
            EnemyManagerInstance = gameObject.GetComponent<EnemyManager>();
    }
    #endregion

    public GameObject enemyCameraPrefab;
    public GameObject enemyPhonePrefab;
    public GameObject enemyHeadphonesPrefab;

    public enum enemyPrefabs
    {
        enemyCameraPrefab,
        enemyPhonePrefab,
        enemyHeadphonesPrefab
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadEnemies(float numberOfEnemies)
    {
        MapTile[] freeSlots = MapManager.MapManagerInstance.GetMapTiles(MapTile.TileType.Floor);
        for (int enemies = 0; enemies < numberOfEnemies; ++enemies)
        {
            MapTile freeSlot = freeSlots[Random.Range(0, freeSlots.Length)];
            MapManager.MapManagerInstance.GetMapTile(freeSlot.logicPosition_X, freeSlot.logicPosition_Y).tileType = MapTile.TileType.Enemy;
            Vector3 spawnPos = new Vector3(freeSlot.transform.position.x, freeSlot.transform.position.y, 0);
            EnemyManager.enemyPrefabs enemy = (EnemyManager.enemyPrefabs)Random.Range(0, 3);
            
            switch(enemy)
            {
                case enemyPrefabs.enemyCameraPrefab:
                    GameObject.Instantiate(enemyCameraPrefab, spawnPos, Quaternion.identity);
                    break;
                case enemyPrefabs.enemyPhonePrefab:
                    GameObject.Instantiate(enemyPhonePrefab, spawnPos, Quaternion.identity);
                    break;
                case enemyPrefabs.enemyHeadphonesPrefab:
                    GameObject.Instantiate(enemyHeadphonesPrefab, spawnPos, Quaternion.identity);
                    break;
            }
            GameManager.GameManagerInstance.addEnemy();
        }
    }
}
