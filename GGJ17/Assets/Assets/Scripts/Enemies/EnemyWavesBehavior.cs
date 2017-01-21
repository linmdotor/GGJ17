using UnityEngine;
using System.Collections;

public class EnemyWavesBehavior : MonoBehaviour {

	public float weaponTimeOn;
	public float weaponTimeOff;

	public WaveEffect[] waves;

	private float currentTime = 0f;
	public bool startEnabled = false;
	private bool weaponEnabled;

	// Use this for initialization
	void Start () {
		currentTime = 0f;
		weaponEnabled = startEnabled;
		SetWeaponEnabled(startEnabled);
	}
	
	// Update is called once per frame
	void Update () {

		currentTime += Time.deltaTime;

		if(weaponEnabled && currentTime >= weaponTimeOn)
		{
			weaponEnabled = false;
			SetWeaponEnabled(weaponEnabled);
			currentTime -= weaponTimeOn;
        }
		else if(!weaponEnabled && currentTime >= weaponTimeOff)
		{
			weaponEnabled = true;
			SetWeaponEnabled(weaponEnabled);
			currentTime -= weaponTimeOff;
		}
    }


	private void SetWeaponEnabled(bool enabled)
	{
		foreach(WaveEffect wave in waves)
			wave.SetEnabledWaves(enabled);
	}
}
