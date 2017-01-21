using UnityEngine;
using System.Collections;

public class WaveEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetEnabledWaves(bool enabled)
	{
		this.gameObject.SetActive(enabled);
	}
}
