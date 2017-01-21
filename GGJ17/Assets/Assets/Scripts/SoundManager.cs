using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


    #region Singleton
    public static SoundManager SoundManagerInstance;

    void Awake()
    {
        if (SoundManagerInstance == null)
            SoundManagerInstance = gameObject.GetComponent<SoundManager>();
    }
    #endregion

    public AudioClip soundTrack;

    public AudioClip playerHit;
    public AudioClip playerDamage;
    public AudioClip playerDeath;

    public AudioClip enemyMusic;
    public AudioClip enemyDeath;
    public AudioClip showcaseCrash;
    public AudioClip emisorMusic;
    public AudioClip emisorCrash;

    public AudioClip getSoundTrack()
    {
        return soundTrack;
    }

    public AudioClip getPlayerHit()
    {
        return playerHit;
    }

    public AudioClip getPlayerDamage()
    {
        return playerDamage;
    }

    public AudioClip getPlayerDeath()
    {
        return playerDeath;
    }

    public AudioClip getEnemyMusic()
    {
        return enemyMusic;
    }

    public AudioClip getEnemyDeath()
    {
        return enemyDeath;
    }

    public AudioClip getShowcaseCrash()
    {
        return showcaseCrash;
    }

    public AudioClip getEmisorMusic()
    {
        return emisorMusic;
    }

    public AudioClip getEmisorCrash()
    {
        return emisorCrash;
    }

}
