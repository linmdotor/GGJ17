using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    public float m_playerSpeed = 5.0f;
    public GameObject attack;
    public Camera camera;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag(KeyCodes.MainCamera).GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 toLook = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        toLook.Normalize();

        float rot_z = Mathf.Atan2(toLook.y, toLook.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if (Input.GetButton(KeyCodes.Up))
        {
            this.transform.position += new Vector3(0, Time.deltaTime * m_playerSpeed, 0);
        }
        if (Input.GetButton(KeyCodes.Down))
        {
            this.transform.position += new Vector3(0, Time.deltaTime * -m_playerSpeed, 0);
        }
        if (Input.GetButton(KeyCodes.Left))
        {
            this.transform.position += new Vector3(Time.deltaTime * -m_playerSpeed, 0, 0);
        }
        if (Input.GetButton(KeyCodes.Right))
        {
            this.transform.position += new Vector3(Time.deltaTime * m_playerSpeed, 0, 0);
        }

        if (Input.GetButtonDown(KeyCodes.Attack) && !attack.activeInHierarchy)
        {
            attack.SetActive(true);
            attack.transform.localPosition = new Vector3(0, 1.75f, 0);
            attack.transform.Rotate(0, 0, 0);
        }
        }
    }
}
