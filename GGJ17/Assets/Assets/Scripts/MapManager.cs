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

    // Size of the current map [X,Y]
    public uint mapSize_X = 50;
    public uint mapSize_Y = 50;

    // Map sprites
    public Sprite spriteFloor;
    public Sprite spriteWall;

	// Use this for initialization
	void Start ()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        CreateMap();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void CreateMap()
    {
        // Map base tiles (Floor)
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

        // Map border tiles (Wall)
        for (int i = 0; i < mapSize_X; ++i)
        {
            tiles[i][0].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
            tiles[i][mapSize_X - 1].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
        }

        for (int j = 0; j < mapSize_Y; ++j)
        {
            tiles[0][j].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
            tiles[mapSize_Y - 1][j].gameObject.GetComponent<SpriteRenderer>().sprite = spriteWall;
        }
    }

}
