using UnityEngine;
using System.Collections;

public class DestroySound : MonoBehaviour {

    private enum EnemyType { thing, guy };

    [SerializeField]
    private EnemyType enemyType;

    // Use this for initialization
    void Start()
    {
        if (enemyType == EnemyType.thing)
        {
            this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getEmisorCrash();
            this.GetComponent<AudioSource>().Play();
        }
        else if (enemyType == EnemyType.guy)
        {
            this.GetComponent<AudioSource>().clip = SoundManager.SoundManagerInstance.getEnemyDeath();
            this.GetComponent<AudioSource>().Play();
        }
    }
}
