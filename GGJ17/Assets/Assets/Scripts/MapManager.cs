using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    // Maximum size for every map [X,Y]
    public int maxMapSize_X = 100;
    public int maxMapSize_Y = 70;

    // Actual size of the map [X,Y]
    public int mapSize_X = 80;
    public int mapSize_Y = 50;

    // Map sprites
    public Sprite spriteFloor;
    public Sprite spriteWall;

    // Current map level and difficulty (depending on the map level, it will be
    // used for the number of things -walls, furnitures, etc.- to instantiate)
    public int[] levelsIncreasingDifficulty = new int[] { 1, 5, 10 };

    private int numDifficultyLevels;
    private int currentDifficultyLevel;
    private int currentLevel;

    // Map internal stuff
    public int[] minWalls = new int[] { 2, 1, 3 };
    public int[] minFurnitures = new int[] { 1, 3, 7 };
    public int[] minCabinets = new int[] { 0, 1, 3 };

    public int[] maxWalls = new int[] { 2, 3, 5 };
    public int[] maxFurnitures = new int[] { 3, 5, 9 };
    public int[] maxCabinets = new int[] { 1, 3, 5 };

    private int numWalls;
    private int numFurnitures;
    private int numCabinets;

    private List<MapTile> walls;
    private List<MapTile> furnitures;
    private List<MapTile> cabinets;

    // Limit dimensions and restrictions for walls
    public int distanceBetweenWalls = 3;
    public int wallMinGap = 3;

    private bool verticalWalls;

    //public int wallMinSize_ShorterDim = 1; // Minimum number of tiles for the shorter dimension of the wall
    //public int wallMinSize_LongerDim = 3;  // Minimum number of tiles for the longer dimension of the wall

    //public int wallMinGap_X = 3; // Minimum number of free tiles for the X dimension of the wall row
    //public int wallMinGap_Y = 2; // Minimum number of free tiles for the Y dimension of the wall column

	// Use this for initialization
	void Start ()
    {
        // Difficulty levels
        numDifficultyLevels = levelsIncreasingDifficulty.Length;

        map = GameObject.FindGameObjectWithTag("Map");
        CreateMap();

        // Global camera (showing the whole map)
        //SetCamera();

        // Map internal stuff instantiation
        verticalWalls = (mapSize_X > mapSize_Y) ? true : false; // Walls direction
        walls = new List<MapTile>();

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
	}
	
	// Update is called once per frame
	void Update ()
    {
	
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

    private void CreateMap()
    {
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
        numWalls = Random.Range(minWalls[currentDifficultyLevel], maxWalls[currentDifficultyLevel] + 1);
        for (int i = 0; i < numWalls; ++i)
            CreateWall();

        numFurnitures = Random.Range(minFurnitures[currentDifficultyLevel], maxFurnitures[currentDifficultyLevel] + 1);
        for (int i = 0; i < numFurnitures; ++i)
            CreateFurniture();

        numCabinets = Random.Range(minCabinets[currentDifficultyLevel], maxCabinets[currentDifficultyLevel] + 1);
        for (int i = 0; i < numCabinets; ++i)
            CreateCabinet();
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

    }

    private void CreateCabinet()
    {

    }

    private MapTile GetRandomTileWall()
    {
        MapTile wallTile = null;

        while (wallTile == null)
        {
            int rndPos_X = Random.Range(0, mapSize_X);
            int rndPos_Y = Random.Range(0, mapSize_Y);

            if ((rndPos_X > wallMinGap) && (rndPos_Y > wallMinGap)
                && (rndPos_X < mapSize_X - wallMinGap) && (rndPos_Y < mapSize_Y - wallMinGap)
                && (tiles[rndPos_X][rndPos_Y].tileType == MapTile.TileType.Floor))
            {
                wallTile = tiles[rndPos_X][rndPos_Y];

                // Distance check between walls
                foreach (MapTile wall in walls)
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

        return wallTile;
    }

}
