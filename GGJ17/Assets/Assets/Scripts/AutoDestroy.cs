using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    public float lifeTime;
    private float _elapsedLifeTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _elapsedLifeTime += Time.deltaTime;

        if (_elapsedLifeTime > lifeTime)
            Destroy(gameObject);

    }
}
