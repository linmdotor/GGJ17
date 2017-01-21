using UnityEngine;
using System.Collections;

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
    public GameObject tilePrefab;

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

    private GameObject[] walls;
    private GameObject[] furnitures;
    private GameObject[] cabinets;

    // Limit dimensions and restrictions for walls
    public int wallMinSize_ShorterDim = 1; // Minimum number of tiles for the shorter dimension of the wall
    public int wallMinSize_LongerDim = 3;  // Minimum number of tiles for the longer dimension of the wall

    public int wallMinGap_X = 3; // Minimum number of free tiles for the X dimension of the wall row
    public int wallMinGap_Y = 2; // Minimum number of free tiles for the Y dimension of the wall column

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

    private void CreateMap()
    {
        // ===== Map base tiles (Floor) =====
        tiles = new MapTile[mapSize_X][];

        for (int i = 0; i < mapSize_X; ++i)
        {
             tiles[i] = new MapTile[mapSize_Y];

            for (int j = 0; j < mapSize_Y; ++j)
            {
                GameObject newTileInstance = (GameObject)Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newTileInstance.name = tilePrefab.name + "_" + i + "_" + j;
                newTileInstance.transform.parent = map.transform;
                
                tiles[i][j] = newTileInstance.GetComponent<MapTile>();
                tiles[i][j].SetPosition(i, j);
            }
        }

        // ===== Map border tiles (Wall) =====
        for (int i = 0; i < mapSize_X; ++i)
        {
            tiles[i][0].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
            tiles[i][0].tileType = MapTile.TileType.Wall;

            tiles[i][mapSize_Y - 1].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
            tiles[i][mapSize_Y - 1].tileType = MapTile.TileType.Wall;
        }

        for (int j = 0; j < mapSize_Y; ++j)
        {
            tiles[0][j].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
            tiles[0][j].tileType = MapTile.TileType.Wall;

            tiles[mapSize_X - 1][j].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
            tiles[mapSize_X - 1][j].tileType = MapTile.TileType.Wall;
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
        MapTile initialTile = GetRandomTile();

        // Wall dimensions
        int wallDim_X;
        int wallDim_Y;

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

        // Wall instantiation
        ObjectManager.ObjectManagerInstance.instantiateWall(initialTile.gameObject, wallDim_X, wallDim_Y);
    }

    private void CreateFurniture()
    {

    }

    private void CreateCabinet()
    {

    }

    private MapTile GetRandomTile()
    {
        while (true)
        {
            int rndPos_X = Random.Range(0, mapSize_X);
            int rndPos_Y = Random.Range(0, mapSize_Y);

            if (tiles[rndPos_X][rndPos_Y].tileType == MapTile.TileType.Floor)
                return tiles[rndPos_X][rndPos_Y];
        }
    }

}
