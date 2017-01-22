using UnityEngine;
using System.Collections;

public class FurniturePiece : MonoBehaviour {

    public bool isCabinet = false;

    public bool emisorPlace = false;
    public int x;
    public int y;
    private bool alreadyBroken = false;

    private void Start()
    {
        if(this.GetComponent<AudioSource>() != null)
        {
            this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getShowcaseCrash();
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isCabinet && !alreadyBroken)
        {
            if(coll.CompareTag(KeyCodes.PlayerAttack))
            {
                if (this.GetComponent<AudioSource>() != null)
                {
                    this.GetComponent<AudioSource>().Play();
                }
                GetComponent<Animator>().enabled = true;
                alreadyBroken = true;
            }
        }
    }
}
