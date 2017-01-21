using UnityEngine;
using System.Collections;

public class FurniturePiece : MonoBehaviour {

    public bool isCabinet = false;

    public bool emisorPlace = false;
    public int x;
    public int y;
    private bool alreadyBroken = false;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isCabinet && !alreadyBroken)
        {
            if(coll.CompareTag(KeyCodes.PlayerAttack))
            {
                Debug.Log("Playerattac");
                GetComponent<Animator>().enabled = true;
                alreadyBroken = true;
            }
        }
    }
}
