using UnityEngine;
using System.Collections;

public class WaveEffect : MonoBehaviour {

    public float originalScaleX = 0.03f;
    public float originalScaleY = 0.03f;
    public float originalScaleXCircle = 0.5f;
    public float originalScaleYCircle = 0.5f;
    public float lifeTimeBase = 5;
    public float lifeTime;
    public bool expand = false;

	// Use this for initialization
	void Start () {
        lifeTime = lifeTimeBase;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetEnabledWaves(bool enabled)
	{
        expand = true;
	}
}
