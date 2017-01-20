using UnityEngine;
using System.Collections;

public class Furniture : MonoBehaviour {

    public int horizontalSize;
    public int verticalSize;
    public ObjectManager.FurnitureType furnitureType;
    public Sprite usedSprite;


    public void setData(int horizontalSize,int verticalSize, ObjectManager.FurnitureType furnitureType,Sprite usedSprite)
    {
        this.horizontalSize = horizontalSize;
        this.verticalSize = verticalSize;
        this.furnitureType = furnitureType;
        this.usedSprite = usedSprite;
    }

    public int createEmisors()
    {

        return 0;
    }
}
