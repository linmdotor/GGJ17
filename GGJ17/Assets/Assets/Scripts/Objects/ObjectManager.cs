using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour {

    #region Singleton
    public static ObjectManager ObjectManagerInstance;

    void Awake()
    {
        if (ObjectManagerInstance == null)
            ObjectManagerInstance = gameObject.GetComponent<ObjectManager>();
    }
    #endregion
    [System.Serializable]
    public class FurnitureSprites
    {
        public Sprite sprite;
        public FurnitureType furnitureSprites;
    }

    public enum FurnitureType
    {
        MADERA,
    };



    Vector3 newfurniturePos = Vector3.zero;

    [SerializeField]
    public List<FurnitureSprites> furnitureSprites;

    private List<Furniture> furnitures = new List<Furniture>();
    private List<Furniture> cabinets = new List<Furniture>();

    public Sprite cabinetSprite;
    public Sprite wallSprite;
    int numbersOfEmissors = 0;

    public GameObject enemyEmissor;

    void Start () {
        //instantiateFurniture(tilePadre,5,3,FurnitureType.MADERA);

	}


    public void instantiateEmisors()
    {
        foreach(Furniture furniture in furnitures)
        {
            int emissor = furniture.createEmisors();
            numbersOfEmissors += emissor;
        }       

    }

    public void instantiateFurniture(GameObject tile, int distanciaHorizontal, int distanciaVertical)
    {
        GameObject mueble = new GameObject("furniture");
        mueble.transform.parent = tile.transform;
        mueble.transform.localPosition = Vector3.zero;
        Sprite selectedSprite = getRandomSpriteFrom(FurnitureType.MADERA);

        mueble.AddComponent<Furniture>().setData(distanciaHorizontal, distanciaVertical, FurnitureType.MADERA, selectedSprite);
        furnitures.Add(mueble.GetComponent<Furniture>());

        for(int i = 0; i < distanciaVertical; i++ )
        {
            for(int j = 0; j < distanciaHorizontal; j++)
            {
                GameObject piezaMueble = new GameObject("piezaMueble" + j + "-" + i);
                piezaMueble.transform.parent = mueble.transform;
                piezaMueble.transform.localPosition = Vector3.zero;
                piezaMueble.AddComponent<FurniturePiece>();
                MapTile mapTile = tile.GetComponent<MapTile>();


                MapManager.MapManagerInstance.GetMapTile(mapTile.logicPosition_X + (uint)j, mapTile.logicPosition_Y +(uint)i).tileType = MapTile.TileType.Furniture;

                if (i == 0 || i == distanciaVertical - 1)
                {
                    piezaMueble.GetComponent<FurniturePiece>().emisorPlace = true;
                    piezaMueble.GetComponent<FurniturePiece>().x = j;
                    piezaMueble.GetComponent<FurniturePiece>().y = i;

                }
                else if (j == 0 || j == distanciaHorizontal - 1)
                {
                    piezaMueble.GetComponent<FurniturePiece>().emisorPlace = true;
                    piezaMueble.GetComponent<FurniturePiece>().x = j;
                    piezaMueble.GetComponent<FurniturePiece>().y = i;
                }

                newfurniturePos.x = piezaMueble.transform.position.x + j * MapTile.TileLength;
                newfurniturePos.y = piezaMueble.transform.position.y + i * -MapTile.TileLength;
                piezaMueble.transform.position = newfurniturePos;
                piezaMueble.AddComponent<SpriteRenderer>().sprite = selectedSprite;
                
                piezaMueble.GetComponent<SpriteRenderer>().sortingLayerName = "Furniture";

                piezaMueble.AddComponent<BoxCollider2D>();

                piezaMueble.GetComponent<BoxCollider2D>().isTrigger = true;
                piezaMueble.tag = KeyCodes.Furniture;

                mueble.GetComponent<Furniture>().furniturePieces.Add(piezaMueble.GetComponent<FurniturePiece>());
                mueble.GetComponent<Furniture>().accessiblePieces++;

            }
        }
    }

    public void instantiateCabinet(GameObject tile, int distanciaHorizontal, int distanciaVertical)
    {
        GameObject cabinet = new GameObject("cabinet");
        cabinet.transform.parent = tile.transform;
        cabinet.transform.localPosition = Vector3.zero;

        cabinet.AddComponent<Furniture>().setData(distanciaHorizontal, distanciaVertical, FurnitureType.MADERA, cabinetSprite);
        cabinets.Add(cabinet.GetComponent<Furniture>());

        for (int i = 0; i < distanciaVertical; i++)
        {
            for (int j = 0; j < distanciaHorizontal; j++)
            {
                GameObject piezaMueble = new GameObject("piezaMueble" + j + "-" + i);
                piezaMueble.transform.parent = cabinet.transform;
                piezaMueble.transform.localPosition = Vector3.zero;
                piezaMueble.AddComponent<FurniturePiece>();
                MapTile mapTile = tile.GetComponent<MapTile>();


                MapManager.MapManagerInstance.GetMapTile(mapTile.logicPosition_X + (uint)j, mapTile.logicPosition_Y + (uint)i).tileType = MapTile.TileType.Cabinet;

                if (i == 0 || i == distanciaVertical - 1)
                {
                    piezaMueble.GetComponent<FurniturePiece>().emisorPlace = true;
                    piezaMueble.GetComponent<FurniturePiece>().x = j;
                    piezaMueble.GetComponent<FurniturePiece>().y = i;

                }
                else if (j == 0 || j == distanciaHorizontal - 1)
                {
                    piezaMueble.GetComponent<FurniturePiece>().emisorPlace = true;
                    piezaMueble.GetComponent<FurniturePiece>().x = j;
                    piezaMueble.GetComponent<FurniturePiece>().y = i;
                }

                newfurniturePos.x = piezaMueble.transform.position.x + j * MapTile.TileLength;
                newfurniturePos.y = piezaMueble.transform.position.y + i * -MapTile.TileLength;
                piezaMueble.transform.position = newfurniturePos;
                piezaMueble.AddComponent<SpriteRenderer>().sprite = cabinetSprite;

                piezaMueble.GetComponent<SpriteRenderer>().sortingLayerName = "Furniture";

                cabinet.GetComponent<Furniture>().furniturePieces.Add(piezaMueble.GetComponent<FurniturePiece>());
                cabinet.GetComponent<Furniture>().accessiblePieces++;

            }
        }
    }

    public void instantiateWall(GameObject tile, int distanciaHorizontal, int distanciaVertical)
    {
        GameObject wall = new GameObject("wall");
        wall.transform.parent = tile.transform;
        wall.transform.localPosition = Vector3.zero;

        for (int i = 0; i < distanciaVertical; i++)
        {
            for (int j = 0; j < distanciaHorizontal; j++)
            {
                GameObject piezaWall = new GameObject("piezaWall" + j + "-" + i);
                piezaWall.transform.parent = wall.transform;
                piezaWall.transform.localPosition = Vector3.zero;
                MapTile mapTile = tile.GetComponent<MapTile>();

                MapManager.MapManagerInstance.GetMapTile(mapTile.logicPosition_X + (uint)j, mapTile.logicPosition_Y + (uint)i).tileType = MapTile.TileType.Wall;

                newfurniturePos.x = piezaWall.transform.position.x + j * MapTile.TileLength;
                newfurniturePos.y = piezaWall.transform.position.y + i * -MapTile.TileLength;
                piezaWall.transform.position = newfurniturePos;
                piezaWall.AddComponent<SpriteRenderer>().sprite = wallSprite;

                piezaWall.GetComponent<SpriteRenderer>().sortingLayerName = "Furniture";

                piezaWall.AddComponent<BoxCollider2D>();
            }
        }
    }


    private Sprite getRandomSpriteFrom(FurnitureType givenType)
    {
        List<FurnitureSprites> posibleSprites = new List<FurnitureSprites>();
        foreach(FurnitureSprites furnitureSprite in furnitureSprites)
        {
            if (furnitureSprite.furnitureSprites == givenType)
            {
                posibleSprites.Add(furnitureSprite);
            }
        }
        return posibleSprites[Random.Range(0, posibleSprites.Count)].sprite;
    }
}
