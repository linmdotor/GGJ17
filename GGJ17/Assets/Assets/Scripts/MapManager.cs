using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
    // GameObject map
    private GameObject map;

    // Matrix of tiles
    private MapTile[][] tiles;

    // GameObject tile
    public GameObject tilePrefab;

    // Maximum size for every map [X,Y]
    public uint maxMapSize_X = 100;
    public uint maxMapSize_Y = 70;

    // Size of the current map [X,Y]
    public uint mapSize_X = 80;
    public uint mapSize_Y = 50;

    // Map sprites
    public Sprite spriteFloor;
    public Sprite spriteWall;

	// Use this for initialization
	void Start ()
    {
        map = GameObject.FindGameObjectWithTag("Map");

        CreateMap();
        SetCamera();

        ObjectManager.ObjectManagerInstance.instantiateFurniture(tiles[20][15].gameObject,4,6,ObjectManager.FurnitureType.MADERA);
        ObjectManager.ObjectManagerInstance.instantiateFurniture(tiles[3][2].gameObject, 2, 1, ObjectManager.FurnitureType.MADERA);
        ObjectManager.ObjectManagerInstance.instantiateFurniture(tiles[50][2].gameObject, 7, 10, ObjectManager.FurnitureType.MADERA);
        ObjectManager.ObjectManagerInstance.instantiateFurniture(tiles[4][30].gameObject, 3, 3, ObjectManager.FurnitureType.MADERA);
        ObjectManager.ObjectManagerInstance.instantiateFurniture(tiles[23][2].gameObject, 2, 6, ObjectManager.FurnitureType.MADERA);
        ObjectManager.ObjectManagerInstance.instantiateFurniture(tiles[3][10].gameObject, 8, 2, ObjectManager.FurnitureType.MADERA);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void CreateMap()
    {
        // ===== Map base tiles (Floor) =====
        tiles = new MapTile[mapSize_X][];

        for (uint i = 0; i < mapSize_X; ++i)
        {
             tiles[i] = new MapTile[mapSize_Y];

            for (uint j = 0; j < mapSize_Y; ++j)
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
            tiles[i][mapSize_Y - 1].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
        }

        for (int j = 0; j < mapSize_Y; ++j)
        {
            tiles[0][j].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
            tiles[mapSize_X - 1][j].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
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

}
