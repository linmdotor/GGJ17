﻿using UnityEngine;
using System.Collections;

public class WaveEffectCircular: WaveEffect
{

	[Header("Circles & Twist")]
	public Transform Circle1;
	public Transform Circle2;
	public float angularSpeed1 = 100.0f;
	public float angularSpeed2 = -100.0f;	

	[Header("Wave (in-out) effect")]
	private Transform[] spriteWaveTransforms = new Transform[12];
	private float initialPosY;
	public float marginPos = 0.5f;
	public float speedDispl = 7.0f;

    public float growthX = 0.3f;
    public float growthY = 0.3f;

    private float red = 0;
    private bool blue = true;


	// Use this for initialization
	void Start () {

		//Store the initial position of the Wave
		initialPosY = Circle1.FindChild("Wave0").FindChild("wave_sprite").transform.localPosition.y;

		//Find all the sprite waves
		for (int i = 0; i < 6; ++i)
		{
			spriteWaveTransforms[i] = Circle1.FindChild("Wave" + i).FindChild("wave_sprite").transform;
			spriteWaveTransforms[i + 6] = Circle2.FindChild("Wave" + i).FindChild("wave_sprite").transform;
		}

        foreach (Transform child in this.transform)
        {
            if (child.transform.GetChild(0).transform.childCount == 0)
                child.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 5);
            else
                child.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 5);
        }

	}
	
	// Update is called once per frame
	void Update () {

        if (expand)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime > 0)
                this.transform.localScale = new Vector3(this.transform.localScale.x + Time.deltaTime * growthX, this.transform.localScale.y + Time.deltaTime * growthY, 0);
            else
            {
                lifeTime = lifeTimeBase;
                expand = false;
                this.transform.localScale = new Vector3(originalScaleXCircle, originalScaleYCircle, 0);
            }
        }

		Circle1.Rotate(Vector3.forward * Time.deltaTime * angularSpeed1);
		Circle2.Rotate(Vector3.forward * Time.deltaTime * angularSpeed2);

		foreach(Transform t in spriteWaveTransforms)
			t.localPosition = new Vector3(0, initialPosY + (marginPos * Mathf.Sin(Time.time * speedDispl)), 0);

        foreach (Transform child in this.transform) 
        { 
            if (blue)
            {
                red += Time.deltaTime;
                Color wave = new Color(red, 0, 5);
                if (child.transform.GetChild(0).transform.childCount == 0)
                    child.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
                else
                    child.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
            }
            if (red >= 10)
                blue = false;
            if (!blue)
            {
                red -= Time.deltaTime;
                Color wave = new Color(red, 0, 5);
                if (child.transform.GetChild(0).transform.childCount == 0)
                    child.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
                else
                    child.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
            }
            if (red <= 0)
                blue = true;
        }
        print(red);
    }
}
