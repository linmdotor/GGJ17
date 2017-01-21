using UnityEngine;
using System.Collections;

public class EnemyWavesBehavior : MonoBehaviour {

	public float weaponTimeOn;
	public float weaponTimeOff;

	public float randomDelayedStartTime = 5f;
	public bool startEnabled = false;
	private bool weaponEnabled;

	public WaveEffect[] waves;

	private float currentTime = 0f;
	

	// Use this for initialization
	void Start () {
		weaponEnabled = startEnabled;
		SetWeaponEnabled(startEnabled);
		randomDelayedStartTime *= Random.value;
		//Hack to start delayed
		currentTime = -randomDelayedStartTime;
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
