using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

    [SerializeField]
    public Sprite[] bloodSprites;

	// Use this for initialization
	void Start () {

        this.GetComponent<SpriteRenderer>().sprite = bloodSprites[Random.Range(0,bloodSprites.Length)];

	}

}
