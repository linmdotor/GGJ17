using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

    [SerializeField]
    public Sprite[] bloodSprites;

	// Use this for initialization
	void Start () {

        this.transform.RotateAround(this.transform.position, new Vector3(0,0,1),(float)Random.Range(0,360)); 
        this.GetComponent<SpriteRenderer>().sprite = bloodSprites[Random.Range(0,bloodSprites.Length)];

	}

}
