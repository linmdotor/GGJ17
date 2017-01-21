using UnityEngine;
using System.Collections;

public class WaveEffectSimple : WaveEffect
{

	[Header("Waves")]
	public Transform[] spriteWaveTransforms;

	[Header("Scale")]
	private Vector3 initialScale;
	public float marginScale = 0.2f;
	public float[] speedScaleX;
	public float[] speedScaleY;

	// Use this for initialization
	void Start () {

		initialScale = spriteWaveTransforms[0].localScale;

	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < spriteWaveTransforms.Length; ++i)
		{
			spriteWaveTransforms[i].localScale = initialScale + new Vector3(marginScale * Mathf.Sin(Time.time * speedScaleX[i]), marginScale * Mathf.Sin(Time.time * speedScaleY[i]), 0);
		}
	}
}
