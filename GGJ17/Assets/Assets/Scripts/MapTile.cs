using UnityEngine;
using System.Collections;

public class MapTile : MonoBehaviour {

    private const float TILE_LENGTH = 500 / 100;

    public static float TileLength
    {
        get
        {
            return TILE_LENGTH;
        }
    }

    public enum TileType
    {
        Floor,
        Wall,
        Furniture, // Muebles
        Cabinet    // Vitrinas
    };

    public TileType tileType = TileType.Floor;

    [HideInInspector]
    public uint logicPosition_X;
    [HideInInspector]
    public uint logicPosition_Y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPosition(uint logicPos_X, uint logicPos_Y)
    {
        logicPosition_X = logicPos_X;
        logicPosition_Y = logicPos_Y;

        float realPosition_X = TILE_LENGTH * (logicPosition_X + 0.5f);
        float realPosition_Y = TILE_LENGTH * (logicPosition_Y + 0.5f);

        transform.position = new Vector3(realPosition_X, -realPosition_Y, 0);
    }

}
