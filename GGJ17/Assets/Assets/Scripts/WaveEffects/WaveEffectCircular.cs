using UnityEngine;
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

	}
	
	// Update is called once per frame
	void Update () {

		Circle1.Rotate(Vector3.forward * Time.deltaTime * angularSpeed1);
		Circle2.Rotate(Vector3.forward * Time.deltaTime * angularSpeed2);

		foreach(Transform t in spriteWaveTransforms)
			t.localPosition = new Vector3(0, initialPosY + (marginPos * Mathf.Sin(Time.time * speedDispl)), 0);
			
	}
}
