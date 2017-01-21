using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    public bool movementPressed = false;
    public float m_playerSpeed = 5.0f;
    public GameObject attack;
    public Camera camera;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag(KeyCodes.MainCamera).GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        movementPressed = false;
        Vector3 toLook = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        toLook.Normalize();

        float rot_z = Mathf.Atan2(toLook.y, toLook.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if (Input.GetButton(KeyCodes.Up))
        {
            this.gameObject.GetComponent<Animator>().SetBool("Walk", true);
            this.transform.position += new Vector3(0, Time.deltaTime * m_playerSpeed, 0);
            movementPressed = true;
        }
        if (Input.GetButton(KeyCodes.Down))
        {
            this.gameObject.GetComponent<Animator>().SetBool("Walk", true);
            this.transform.position += new Vector3(0, Time.deltaTime * -m_playerSpeed, 0);
            movementPressed = true;
        }
        if (Input.GetButton(KeyCodes.Left))
        {
            this.gameObject.GetComponent<Animator>().SetBool("Walk", true);
            this.transform.position += new Vector3(Time.deltaTime * -m_playerSpeed, 0, 0);
            movementPressed = true;
        }
        if (Input.GetButton(KeyCodes.Right))
        {
            this.gameObject.GetComponent<Animator>().SetBool("Walk", true);
            this.transform.position += new Vector3(Time.deltaTime * m_playerSpeed, 0, 0);
            movementPressed = true;
        }
        if (!movementPressed)
            this.gameObject.GetComponent<Animator>().SetBool("Walk", false);
            

        if (Input.GetButtonDown(KeyCodes.PlayerAttack) && !attack.activeInHierarchy)
        {
            attack.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("Attack", true);
        }
    }
}
