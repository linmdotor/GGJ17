using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public int numberEnemies = 10;

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

    public void loadEnemies()
    {
        MapTile[] freeSlots = MapManager.MapManagerInstance.GetMapTiles(MapTile.TileType.Floor);
        for (int enemies = 0; enemies < numberEnemies; ++enemies)
        {
            MapTile freeSlot = freeSlots[Random.Range(0, freeSlots.Length)];
            //SI HAY PROBLEMA AÑADIR CONTROL DE REPETICIÓN DE SPAWN EN MISMA CASILLA.
            Vector3 spawnPos = new Vector3(freeSlot.transform.position.x, freeSlot.transform.position.y, 0);
            EnemyManager.enemyPrefabs enemy = (EnemyManager.enemyPrefabs)Random.Range(0, 2);
            
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
        }
    }
}
