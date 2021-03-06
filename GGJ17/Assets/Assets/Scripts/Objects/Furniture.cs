﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Furniture : MonoBehaviour {

    public int horizontalSize;
    public int verticalSize;
    public ObjectManager.FurnitureType furnitureType;
    public Sprite usedSprite;

    public List<FurniturePiece> furniturePieces;
    public int accessiblePieces = 0;
    private int maxNumberOfEmissors = 0;
    private int currentNumberOfEmissors = 0;

    public void setData(int horizontalSize,int verticalSize, ObjectManager.FurnitureType furnitureType,Sprite usedSprite)
    {

        furniturePieces = new List<FurniturePiece>();
        this.horizontalSize = horizontalSize;
        this.verticalSize = verticalSize;
        this.furnitureType = furnitureType;
        this.usedSprite = usedSprite;
    }

    public int createEmisors()
    {
        maxNumberOfEmissors = accessiblePieces/2;
        int separeteDistance = 2;
        int currentSepareteDistance = 2;

        foreach (FurniturePiece piece in furniturePieces)
       {
            if (currentNumberOfEmissors == maxNumberOfEmissors)
                break;

            if(piece.emisorPlace == true)
            {

                if(currentSepareteDistance != separeteDistance)
                {
                    if(piece.x != 0 || piece.x != horizontalSize)
                        currentSepareteDistance++;

                }
                else
                {
                    //50% de probabilidades de que aparezca
                    if(Random.Range(0,100) < 50)
                    {
                        currentSepareteDistance = 0;
                        GameObject emissor = Instantiate(ObjectManager.ObjectManagerInstance.enemyEmissor);
                        emissor.transform.parent = piece.transform;
                        emissor.transform.localPosition = Vector3.zero;
                        currentNumberOfEmissors++;
                        GameManager.GameManagerInstance.addEnemy();
                    }
                }
            }
        }
        return currentNumberOfEmissors;
    }

    public int createCabinetEmisors()
    {
        maxNumberOfEmissors = accessiblePieces/2;
        int separeteDistance = 2;
        int currentSepareteDistance = 2;

        foreach (FurniturePiece piece in furniturePieces)
       {
            if (currentNumberOfEmissors == maxNumberOfEmissors)
                break;

            if(piece.emisorPlace == true)
            {

                if(currentSepareteDistance != separeteDistance)
                {
                    if(piece.x != 0 || piece.x != horizontalSize)
                        currentSepareteDistance++;

                }
                else
                {
                    //50% de probabilidades de que aparezca
                    if(Random.Range(0,100) < 50)
                    {
                        currentSepareteDistance = 0;
                        GameObject emissor = Instantiate(ObjectManager.ObjectManagerInstance.enemyEmissor);
                        emissor.GetComponent<Enemy>().life = 2;
                        emissor.transform.parent = piece.transform;
                        emissor.transform.localPosition = Vector3.zero;
                        currentNumberOfEmissors++;
                        GameManager.GameManagerInstance.addEnemy();
                    }
                }
            }
        }
        return currentNumberOfEmissors;
    }

}
