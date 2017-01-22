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

    public float growthX = 0.3f;
    public float growthY = 0.3f;

    private float red = 0;
    private bool blue = true;
    private float timeToChangeBase = 0.5f;
    private float timeToChange = 0.5f;

	// Use this for initialization
	void Start () {

		initialScale = spriteWaveTransforms[0].localScale;

        foreach (Transform child in this.transform)
        {
            if (child.transform.GetChild(0).transform.childCount == 0)
                child.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 256/225.0f);
            else
                foreach (Transform childIn in child)
                    childIn.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 256/255.0f);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (expand)
        {
            lifeTime -= Time.deltaTime;
            if(lifeTime > 0)
                this.transform.localScale = new Vector3(this.transform.localScale.x + Time.deltaTime * growthX, this.transform.localScale.y + Time.deltaTime * growthY, 0);
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

        timeToChange -= Time.deltaTime;
        if (timeToChange <= 0) { 
            foreach (Transform child in this.transform)
            {

                int red = Random.Range(0, 256);
                int green = Random.Range(0, 256);
                int blue = Random.Range(0, 256);
                Color32 wave = new Color(red / 255.0f, green / 255.0f, blue / 255.0f, 255f / 255.0f);
                if (child.transform.GetChild(0).transform.childCount == 0)
                    child.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
                else
                    foreach (Transform childIn in child)
                        childIn.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
            }
            timeToChange = timeToChangeBase;
        }
        /*foreach(Transform child in this.transform)
        {
            if (blue)
            {
                red += Time.deltaTime * 10;
                Color wave = new Color(red, 0, 5);
                if (child.transform.GetChild(0).transform.childCount == 0)
                    child.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
                else
                    child.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
            }
            if (red >= 512)
                blue = false;
            if (!blue)
            {
                red -= Time.deltaTime * 10;
                Color wave = new Color(red, 0, 256);
                if (child.transform.GetChild(0).transform.childCount == 0)
                    child.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
                else
                    child.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = wave;
            }
            if (red <= 0)
                blue = true;
        }*/
	}
}
