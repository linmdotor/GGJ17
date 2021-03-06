﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager MapManagerInstance;

    void Awake()
    {
        if (MapManagerInstance == null)
            MapManagerInstance = gameObject.GetComponent<MapManager>();
    }
    #endregion

    // GameObject map
    private GameObject map;

    // Matrix of tiles
    private MapTile[][] tiles;

    // GameObject tile
    public GameObject tileFloorPrefab;
    public GameObject tileWallPrefab;

    // Map sprites
    public Sprite spriteFloor;
    public Sprite spriteWall;

    // Current map level and difficulty (depending on the map level, it will be
    // used for the number of things -walls, furniture, etc.- to instantiate)
    [Header("Difficulty levels and number of objects")]
    public int[] levelsIncreasingDifficulty = new int[] { 1, 5, 10 };

    private int numDifficultyLevels;
    private int currentDifficultyLevel;
    private int currentLevel;

    // Map size
    [Header("Map variable size")]
    // Maximum size for every map [X,Y]
    public int maxMapSize_X = 48;
    public int maxMapSize_Y = 27;

    // Actual size of the map [X,Y]
    private int mapSize_X = 32;
    private int mapSize_Y = 17;

    public int[] minSize_X = new int[] { 28, 35, 42 };
    public int[] maxSize_X = new int[] { 32, 40, 48 };

    public int[] minSize_Y = new int[] { 16, 20, 24 };
    public int[] maxSize_Y = new int[] { 17, 22, 27 };

    // Map internal stuff
    [Header("Map internal stuff")]
    public int[] minWalls = new int[] { 2, 1, 3 };
    public int[] minFurniture = new int[] { 1, 3, 7 };
    public int[] minCabinets = new int[] { 0, 1, 3 };

    public int[] maxWalls = new int[] { 2, 3, 5 };
    public int[] maxFurniture = new int[] { 3, 5, 9 };
    public int[] maxCabinets = new int[] { 1, 3, 5 };

    private int numWalls;
    private int numFurniture;
    private int numCabinets;

    private List<MapTile> walls;
    private List<MapTile> furniture;
    private List<MapTile> cabinets;

    // Limit dimensions and restrictions for WALLS
    [Header("Walls")]
    public int distanceBetweenWalls = 3;
    public int wallMinGap = 3;

    private bool verticalWalls;

    // Limit dimensions and restrictions for FURNITURE
    [Header("Furniture")]
    public int freeTilesSurroundingFurniture = 3;

    public int furnMinSize_ShorterDim = 2; // Minimum number of tiles for the shorter dimension of the furniture
    public int furnMaxSize_ShorterDim = 3; // Maximum number of tiles for the shorter dimension of the furniture
    public int furnMinSize_LongerDim = 2;  // Minimum number of tiles for the longer dimension of the furniture

    // Limit dimensions and restrictions for CABINETS
    [Header("Cabinets")]
    public int freeTilesSurroundingCabinets = 3;

    public int cabSize_FixedDim = 2; // Number of tiles for the fixed dimension of the cabinets
    public int cabMinSize_VbleDim = 1;  // Minimum number of tiles for the variable dimension of the cabinets

    // Enemies
    [Header("Enemies")]
    public int[] minEnemies = new int[] { 10, 15, 20 };
    public int[] maxEnemies = new int[] { 13, 20, 28 };

    // Player
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        // Difficulty levels
        numDifficultyLevels = levelsIncreasingDifficulty.Length;
        currentDifficultyLevel = 0;
        currentLevel = 1;

        // Map and level
        map = GameObject.FindGameObjectWithTag("Map");

        walls = new List<MapTile>();
        furniture = new List<MapTile>();
        cabinets = new List<MapTile>();

        // First level generation
        GenerateLevel(1);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Testing levels dynamic generation
        //StartCoroutine(LevelSeriesGeneration());
	}

    IEnumerator LevelSeriesGeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            GenerateLevel(currentLevel + 1);
        }
    }

    public int GetMapSize_X()
    {
        return mapSize_X;
    }

    public int GetMapSize_Y()
    {
        return mapSize_Y;
    }

    public MapTile GetMapTile(int i, int j)
    {
        return tiles[i][j];
    }

    public MapTile[] GetMapTiles(MapTile.TileType wantedTileType)
    {
        List<MapTile> mapTiles = new List<MapTile>();

        for (int i = 0; i < mapSize_X; ++i)
            for (int j = 0; j < mapSize_Y; ++j)
                if (tiles[i][j].tileType == wantedTileType)
                    mapTiles.Add(tiles[i][j]);

        return mapTiles.ToArray();
    }

    public void GenerateLevel(int level)
    {
        // Level
        currentLevel = level;
        
        int nextDifficultyLevel = currentDifficultyLevel + 1;
        if (nextDifficultyLevel < numDifficultyLevels && currentLevel >= levelsIncreasingDifficulty[nextDifficultyLevel])
            currentDifficultyLevel++;

        // Map
        CleanMap();
        CreateMap();

        // Global camera (showing the whole map)
        //SetCamera();

        // Map internal stuff instantiation
        verticalWalls = (mapSize_X > mapSize_Y) ? true : false; // Walls direction

        if (verticalWalls)
        {
            walls.Add(tiles[0][0]);
            walls.Add(tiles[mapSize_X - 1][0]);
        }
        else
        {
            walls.Add(tiles[0][0]);
            walls.Add(tiles[0][mapSize_Y - 1]);
        }

        CreateMapObjects();

        // Enemies
        int numEnemies = UnityEngine.Random.Range(minEnemies[currentDifficultyLevel], maxEnemies[currentDifficultyLevel] + 1);
        EnemyManager.EnemyManagerInstance.loadEnemies(numEnemies);

        // Player spawn point
        MapTile[] spawnTilePlayer = GetMapTiles(MapTile.TileType.Floor);
        int rand = UnityEngine.Random.Range(0, spawnTilePlayer.Length);
        Vector3 spawnPlayer = new Vector3(spawnTilePlayer[rand].transform.position.x, spawnTilePlayer[rand].transform.position.y, 0);
        GameObject.Instantiate(player, spawnPlayer, Quaternion.identity);
    }

    private void CleanMap()
    {
        //Object
        ObjectManager.ObjectManagerInstance.furnitures.Clear();
        ObjectManager.ObjectManagerInstance.cabinets.Clear();



        // Blood sprites
        List<GameObject> bloodGameObjects = GameObject.FindGameObjectsWithTag("Blood").ToList();
        bloodGameObjects.ForEach(bloodGO => Destroy(bloodGO));

        // Enemies
        GameManager.GameManagerInstance.cleanEnemies();

        // Map tiles
        List<GameObject> mapChildren = new List<GameObject>();

        foreach (Transform child in map.transform)
            mapChildren.Add(child.gameObject);

        mapChildren.ForEach(child => Destroy(child));

        // Map objects
        walls.Clear();
        furniture.Clear();
        cabinets.Clear();
    }

    private void CreateMap()
    {
        // ===== Map size =====
        mapSize_X = UnityEngine.Random.Range(minSize_X[currentDifficultyLevel], maxSize_X[currentDifficultyLevel] + 1);
        mapSize_Y = UnityEngine.Random.Range(minSize_Y[currentDifficultyLevel], maxSize_Y[currentDifficultyLevel] + 1);

        // ===== Map base tiles (Floor) =====
        tiles = new MapTile[mapSize_X][];

        for (int i = 0; i < mapSize_X; ++i)
        {
            tiles[i] = new MapTile[mapSize_Y];

            for (int j = 0; j < mapSize_Y; ++j)
            {
                GameObject newTileInstance = (GameObject)Instantiate(tileFloorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newTileInstance.name = tileFloorPrefab.name + "_" + i + "_" + j;
                newTileInstance.transform.parent = map.transform;
                
                tiles[i][j] = newTileInstance.GetComponent<MapTile>();
                tiles[i][j].SetPosition(i, j);
            }
        }

        // ===== Map border tiles (Wall) =====
        for (int i = 0; i < mapSize_X; ++i)
        {
            // Row 0
            GameObject newTileInstance = (GameObject)Instantiate(tileWallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newTileInstance.name = tileWallPrefab.name + "_" + i + "_" + 0;
            newTileInstance.transform.parent = map.transform;

            Destroy(tiles[i][0].gameObject);

            tiles[i][0] = newTileInstance.GetComponent<MapTile>();
            tiles[i][0].SetPosition(i, 0);

            // Row (mapSize_Y - 1)
            GameObject newTileInstance2 = (GameObject)Instantiate(tileWallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newTileInstance2.name = tileWallPrefab.name + "_" + i + "_" + (mapSize_Y - 1);
            newTileInstance2.transform.parent = map.transform;

            Destroy(tiles[i][mapSize_Y - 1].gameObject);

            tiles[i][mapSize_Y - 1] = newTileInstance2.GetComponent<MapTile>();
            tiles[i][mapSize_Y - 1].SetPosition(i, mapSize_Y - 1);
        }

        for (int j = 0; j < mapSize_Y; ++j)
        {
            // Column 0
            GameObject newTileInstance = (GameObject)Instantiate(tileWallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newTileInstance.name = tileWallPrefab.name + "_" + 0 + "_" + j;
            newTileInstance.transform.parent = map.transform;

            Destroy(tiles[0][j].gameObject);

            tiles[0][j] = newTileInstance.GetComponent<MapTile>();
            tiles[0][j].SetPosition(0, j);

            // Column (mapSize_X - 1)
            GameObject newTileInstance2 = (GameObject)Instantiate(tileWallPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newTileInstance2.name = tileWallPrefab.name + "_" + (mapSize_X - 1) + "_" + j;
            newTileInstance2.transform.parent = map.transform;

            Destroy(tiles[mapSize_X - 1][j].gameObject);

            tiles[mapSize_X - 1][j] = newTileInstance2.GetComponent<MapTile>();
            tiles[mapSize_X - 1][j].SetPosition(mapSize_X - 1, j);
        }
    }

    private void SetCamera()
    {
        // ===== Position =====
        // Camera position for a full map (map size == screen size)
        float maxCameraPos_X = (maxMapSize_X * MapTile.TileLength) / 2;
        float maxCameraPos_Y = (maxMapSize_Y * MapTile.TileLength) / 2;

        // Camera position adjustment
        float cameraPosShift_X = (maxMapSize_X - mapSize_X) * MapTile.TileLength / 2;
        float finalCameraPos_X = maxCameraPos_X - cameraPosShift_X;

        float cameraPosShift_Y = (maxMapSize_Y - mapSize_Y) * MapTile.TileLength / 2;
        float finalCameraPos_Y = maxCameraPos_Y - cameraPosShift_Y;

        Camera.main.transform.position = new Vector3(finalCameraPos_X, -finalCameraPos_Y, Camera.main.transform.position.z);

        // ===== Size =====
        Camera.main.orthographicSize = (maxMapSize_Y * MapTile.TileLength) / 2;
    }

    private void CreateMapObjects()
    {
        numWalls = UnityEngine.Random.Range(minWalls[currentDifficultyLevel], maxWalls[currentDifficultyLevel] + 1);
        for (int i = 0; i < numWalls; ++i)
            CreateWall();

        numFurniture = UnityEngine.Random.Range(minFurniture[currentDifficultyLevel], maxFurniture[currentDifficultyLevel] + 1);
        for (int i = 0; i < numFurniture; ++i)
            CreateFurniture();

        numCabinets = UnityEngine.Random.Range(minCabinets[currentDifficultyLevel], maxCabinets[currentDifficultyLevel] + 1);
        for (int i = 0; i < numCabinets; ++i)
            CreateCabinet();

        ObjectManager.ObjectManagerInstance.instantiateEmisors();
    }

    private void CreateWall()
    {
        // Wall initial tile
        MapTile initialTile = GetRandomTileWall();

        // Wall semi-random dimensions
        int wallDim_X;
        int wallDim_Y;

        if (verticalWalls)
        {
            wallDim_X = 1;

            if ((wallMinGap < initialTile.logicPosition_Y) && (initialTile.logicPosition_Y < mapSize_Y / 2))
            {
                // Wall going down
                wallDim_Y = mapSize_Y - initialTile.logicPosition_Y - 1;
            }
            else
            {
                // Wall going up
                wallDim_Y = initialTile.logicPosition_Y;
                initialTile = tiles[initialTile.logicPosition_X][0];
            }
        }
        else
        {
            wallDim_Y = 1;

            if ((wallMinGap < initialTile.logicPosition_X) && (initialTile.logicPosition_X < mapSize_X / 2))
            {
                // Wall going right
                wallDim_X = mapSize_X - initialTile.logicPosition_X - 1;
            }
            else
            {
                // Wall going left
                wallDim_X = initialTile.logicPosition_X;
                initialTile = tiles[0][initialTile.logicPosition_Y];
            }
        }

        // Wall random dimensions
        /*
        if (Random.Range(0, 2) == 0)
        {
            wallDim_X = wallMinSize_ShorterDim;
            wallDim_Y = wallMinSize_LongerDim +
                Random.Range(0, Mathf.Min(mapSize_Y - wallMinGap_Y, mapSize_Y - initialTile.logicPosition_Y));
        }
        else
        {
            wallDim_Y = wallMinSize_ShorterDim;
            wallDim_X = wallMinSize_LongerDim +
                Random.Range(0, Mathf.Min(mapSize_X - wallMinGap_X, mapSize_X - initialTile.logicPosition_X));
        }
        */

        // Wall instantiation and register
        walls.Add(initialTile);
        ObjectManager.ObjectManagerInstance.instantiateWall(initialTile.gameObject, wallDim_X, wallDim_Y);
    }

    private void CreateFurniture()
    {
        // Maximum number of attempts to avoid infinite loops
        int numAttempts = 0;
        int maxAttempts = 10;

        // Furniture initial tile
        MapTile initialTile = null;

        // Furniture random dimensions
        int furnitureDim_X = 0;
        int furnitureDim_Y = 0;

        while (initialTile == null && numAttempts < maxAttempts)
        {
            initialTile = GetRandomTile(freeTilesSurroundingFurniture);

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                // Shorter dimension == X
                furnitureDim_X = UnityEngine.Random.Range(furnMinSize_ShorterDim, furnMaxSize_ShorterDim + 1);
                furnitureDim_Y = furnMinSize_LongerDim +
                    UnityEngine.Random.Range(0, Mathf.Min(mapSize_Y / 2, mapSize_Y - initialTile.logicPosition_Y - 2));
            }
            else
            {
                // Shorter dimension == Y
                furnitureDim_Y = UnityEngine.Random.Range(furnMinSize_ShorterDim, furnMaxSize_ShorterDim + 1);
                furnitureDim_X = furnMinSize_LongerDim +
                    UnityEngine.Random.Range(0, Mathf.Min(mapSize_X / 2, mapSize_X - initialTile.logicPosition_X - 2));
            }

            if (!CheckTilesSurrounding(initialTile, furnitureDim_X, furnitureDim_Y, freeTilesSurroundingFurniture))
                initialTile = null;

            ++numAttempts;
        }

        if (initialTile != null)
        {
            // Furniture instantiation and register
            furniture.Add(initialTile);
            ObjectManager.ObjectManagerInstance.instantiateFurniture(initialTile.gameObject, furnitureDim_X, furnitureDim_Y);
        }
    }

    private void CreateCabinet()
    {
        // Maximum number of attempts to avoid infinite loops
        int numAttempts = 0;
        int maxAttempts = 10;

        // Cabinet initial tile
        MapTile initialTile = null;

        // Cabinet random dimensions
        int cabinetDim_X = 0;
        int cabinetDim_Y = 0;

        while (initialTile == null && numAttempts < maxAttempts)
        {
            initialTile = GetRandomTile(freeTilesSurroundingCabinets);

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                // Fixed dimension == X
                cabinetDim_X = cabSize_FixedDim;
                cabinetDim_Y = cabMinSize_VbleDim +
                    UnityEngine.Random.Range(0, Mathf.Min(mapSize_Y / 2, mapSize_Y - initialTile.logicPosition_Y - 2));
            }
            else
            {
                // Fixed dimension == Y
                cabinetDim_Y = cabSize_FixedDim;
                cabinetDim_X = cabMinSize_VbleDim +
                    UnityEngine.Random.Range(0, Mathf.Min(mapSize_X / 2, mapSize_X - initialTile.logicPosition_X - 2));
            }

            if (!CheckTilesSurrounding(initialTile, cabinetDim_X, cabinetDim_Y, freeTilesSurroundingCabinets))
                initialTile = null;

            ++numAttempts;
        }

        if (initialTile != null)
        {
            // Cabinet instantiation and register
            cabinets.Add(initialTile);
            ObjectManager.ObjectManagerInstance.instantiateCabinet(initialTile.gameObject, cabinetDim_X, cabinetDim_Y);
        }
    }

    private MapTile GetRandomTileWall()
    {
        MapTile wallTile = null;

        while (wallTile == null)
        {
            int rndPos_X = UnityEngine.Random.Range(0, mapSize_X);
            int rndPos_Y = UnityEngine.Random.Range(0, mapSize_Y);

            if ((rndPos_X > wallMinGap) && (rndPos_Y > wallMinGap)
                && (rndPos_X < mapSize_X - wallMinGap) && (rndPos_Y < mapSize_Y - wallMinGap)
                && (tiles[rndPos_X][rndPos_Y].tileType == MapTile.TileType.Floor))
            {
                wallTile = tiles[rndPos_X][rndPos_Y];

                // Distance check between walls
                foreach (MapTile wall in walls)
                {
                    if (wallTile != null)
                    {
                        if (verticalWalls)
                        {
                            // Check horizontal distance
                            if (Mathf.Abs(wallTile.logicPosition_X - wall.logicPosition_X) < distanceBetweenWalls)
                                wallTile = null;
                        }
                        else
                        {
                            // Check vertical distance
                            if (Mathf.Abs(wallTile.logicPosition_Y - wall.logicPosition_Y) < distanceBetweenWalls)
                                wallTile = null;
                        }
                    }
                }
            }
        }

        return wallTile;
    }

    private MapTile GetRandomTile(int freeTilesSurrounding)
    {
        MapTile randomTile = null;

        while (randomTile == null)
        {
            int rndPos_X = UnityEngine.Random.Range(0, mapSize_X);
            int rndPos_Y = UnityEngine.Random.Range(0, mapSize_Y);

            if ((rndPos_X > freeTilesSurrounding) && (rndPos_Y > freeTilesSurrounding)
                && (rndPos_X < mapSize_X - freeTilesSurrounding) && (rndPos_Y < mapSize_Y - freeTilesSurrounding)
                && (tiles[rndPos_X][rndPos_Y].tileType == MapTile.TileType.Floor))
            {
                randomTile = tiles[rndPos_X][rndPos_Y];
            }
        }

        return randomTile;
    }

    private bool CheckTilesSurrounding(MapTile initialTile, int dimention_X, int dimention_Y, int freeTilesSurrounding)
    {
        // i == initialTile.logicPosition_X
        // i == initialTile.logicPosition_X + dimention_X - 1
        for (int j = initialTile.logicPosition_Y; j < initialTile.logicPosition_Y + dimention_Y; ++j)
        {
            if (!CheckTilesSurrounding(tiles[initialTile.logicPosition_X][j], freeTilesSurrounding))
                return false;
            if (!CheckTilesSurrounding(tiles[initialTile.logicPosition_X + dimention_X - 1][j], freeTilesSurrounding))
                return false;
        }

        // j == initialTile.logicPosition_Y
        // j == initialTile.logicPosition_Y + dimention_Y - 1
        for (int i = initialTile.logicPosition_X; i < initialTile.logicPosition_X + dimention_X; ++i)
        {
            if (!CheckTilesSurrounding(tiles[i][initialTile.logicPosition_Y], freeTilesSurrounding))
                return false;
            if (!CheckTilesSurrounding(tiles[i][initialTile.logicPosition_Y + dimention_Y - 1], freeTilesSurrounding))
                return false;
        }

        return true;
    }
    
    private bool CheckTilesSurrounding(MapTile initialTile, int freeTilesSurrounding)
    {
        for (int n = 1; n <= freeTilesSurrounding; ++n)
        {
            int minPos_X = initialTile.logicPosition_X - n;
            int maxPos_X = initialTile.logicPosition_X + n;

            int minPos_Y = initialTile.logicPosition_Y - n;
            int maxPos_Y = initialTile.logicPosition_Y + n;

            for (int j = minPos_Y; j <= maxPos_Y; ++j)
            {
                for (int i = minPos_X; i <= maxPos_X; ++i)
                {
                    if (tiles[i][j].tileType != MapTile.TileType.Floor)
                        return false;
                }
            }
        }

        return true;
    }

}
