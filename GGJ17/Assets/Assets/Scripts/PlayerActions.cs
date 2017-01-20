using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    public float m_playerSpeed = 5.0f;
    public GameObject attack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton(KeyCodes.Up))
        {
            transform.Translate(0, Time.deltaTime * m_playerSpeed, 0);
        }

        if (Input.GetButton(KeyCodes.Down))
        {
            transform.Translate(0, Time.deltaTime * -m_playerSpeed, 0);
        }

        if (Input.GetButton(KeyCodes.Left))
        {
            transform.Translate(Time.deltaTime * -m_playerSpeed, 0, 0);
        }

        if (Input.GetButton(KeyCodes.Right))
        {
            transform.Translate(Time.deltaTime * m_playerSpeed, 0, 0);
        }

        if(Input.GetButtonDown(KeyCodes.Attack) && !attack.activeInHierarchy) //&& Comprobar si el ataque anterior ha acabado)
        {
            attack.SetActive(true);
        }
    }
}
