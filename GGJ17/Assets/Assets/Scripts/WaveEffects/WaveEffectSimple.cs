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
        if (expand)
        {
            lifeTime -= Time.deltaTime;
            if(lifeTime > 0)
                this.transform.localScale = new Vector3(this.transform.localScale.x + Time.deltaTime * 0.3f, this.transform.localScale.y + Time.deltaTime * 0.3f, 0);
            else
            {
                lifeTime = lifeTimeBase;
                expand = false;
                this.transform.localScale = new Vector3(originalScaleX, originalScaleY, 0); 
            }
        }

		for (int i = 0; i < spriteWaveTransforms.Length; ++i)
		{
			spriteWaveTransforms[i].localScale = initialScale + new Vector3(marginScale * Mathf.Sin(Time.time * speedScaleX[i]), marginScale * Mathf.Sin(Time.time * speedScaleY[i]), 0);
		}
	}
}
