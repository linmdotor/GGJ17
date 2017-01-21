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
        public FurnitureType furnitureType;
    }
    public enum FurnitureType
    {
        MADERA,
        COBRE,
        SALCHICAS
    };

    Vector3 newfurniturePos = Vector3.zero;

    public GameObject tilePadre;

    public float HorDistanceBetweenTiles = 2f;
    public float VertDistanceBetweenTiles = 5f;

    [SerializeField]
    public List<FurnitureSprites> furnitureSprites;

    private List<Furniture> furnitures = new List<Furniture>();
    
    int numbersOfEmissors = 0;

    public GameObject objetoDeMierdaQueVaEnLaMesa;

    void Start () {
        instantiateFurniture(tilePadre,5,3,FurnitureType.MADERA);
        instantiateEmisors(); 
	}


    public void instantiateEmisors()
    {
        foreach(Furniture furniture in furnitures)
        {
            numbersOfEmissors += furniture.createEmisors();
        }
    }

    public void instantiateFurniture(GameObject tile, int distanciaHorizontal, int distanciaVertical, FurnitureType furnitureType)
    {
        GameObject mueble = new GameObject("furniture");

        mueble.transform.parent = tile.transform;
        mueble.transform.localPosition = Vector3.zero;
        Sprite selectedSprite = getRandomSpriteFrom(furnitureType);

        mueble.AddComponent<Furniture>().setData(distanciaHorizontal,distanciaVertical,furnitureType,selectedSprite);
        furnitures.Add(mueble.GetComponent<Furniture>());

        for(int i = 0; i < distanciaVertical; i++ )
        {
            for(int j = 0; j < distanciaHorizontal; j++)
            {
                GameObject piezaMueble = new GameObject("piezbaMueble" + j + "-" + i);
                piezaMueble.transform.parent = mueble.transform;
                piezaMueble.transform.localPosition = Vector3.zero;
                piezaMueble.AddComponent<FurniturePiece>();

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
                    
                newfurniturePos.x = piezaMueble.transform.position.x + j * HorDistanceBetweenTiles;
                newfurniturePos.y = piezaMueble.transform.position.y + i * -VertDistanceBetweenTiles;
                piezaMueble.transform.position = newfurniturePos;
                piezaMueble.AddComponent<SpriteRenderer>().sprite = selectedSprite;

                mueble.GetComponent<Furniture>().furniturePieces.Add(piezaMueble.GetComponent<FurniturePiece>());
                mueble.GetComponent<Furniture>().accessiblePieces++;

            }
        }
    }

    private Sprite getRandomSpriteFrom(FurnitureType givenType)
    {
        List<FurnitureSprites> posibleSprites = new List<FurnitureSprites>();
        foreach(FurnitureSprites furnitureSprite in furnitureSprites)
        {
            if(furnitureSprite.furnitureType == givenType)
            {
                posibleSprites.Add(furnitureSprite);
            }
        }
        return posibleSprites[Random.Range(0, posibleSprites.Count)].sprite;
    }
}
