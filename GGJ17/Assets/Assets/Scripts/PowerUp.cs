using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public float redBullTime = 5;
    public float redBullBoost = 6;
    public float foilTime = 5;
    public int sandwichHeal = 20;

    public int tileXCord;
    public int tileYCord;

    public float expiresOn = 10;
    private float timeOnMap;

    public PowerUpManager.powerUps type;

    public Sprite sandwich;
    public Sprite redbull;
    public Sprite foil;

    private GameObject player;

	// Use this for initialization
	void Start () {
        timeOnMap = expiresOn;
        type = (PowerUpManager.powerUps)Random.Range(0, 3);
        if (type == PowerUpManager.powerUps.Foil)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = foil;
        if (type == PowerUpManager.powerUps.RedBull)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = redbull;
        if (type == PowerUpManager.powerUps.Sandwich)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sandwich;
        player = GameObject.FindGameObjectWithTag(KeyCodes.PlayerWarrior);
	}
	
	// Update is called once per frame
	void Update () {
        timeOnMap -= Time.deltaTime;
        if (timeOnMap <= 0)
            deletePowerUp();
	}

    public void deletePowerUp()
    {
        MapManager.MapManagerInstance.GetMapTile(tileXCord, tileYCord).tileType = MapTile.TileType.Floor;
        Destroy(this.gameObject);
    }
    public void effect()
    {
        switch(this.type)
        {
            case PowerUpManager.powerUps.Foil:
                player.GetComponent<PlayerManager>().foilOn = true;
                player.GetComponent<PlayerManager>().foilTime = foilTime;
                break;
            case PowerUpManager.powerUps.RedBull:
                player.GetComponent<PlayerActions>().redBullBoost = redBullBoost;
                player.GetComponent<PlayerActions>().m_playerSpeed += redBullBoost;
                player.GetComponent<PlayerActions>().redBullOn = true;
                player.GetComponent<PlayerActions>().redBullTime = redBullTime;
                break;
            case PowerUpManager.powerUps.Sandwich:
                if (player.GetComponent<PlayerManager>().life > (100 - sandwichHeal))
                    player.GetComponent<PlayerManager>().life = 100;
                else
                    player.GetComponent<PlayerManager>().life += sandwichHeal;
                UIManager.UIManagerInstance.changeLifeText(player.GetComponent<PlayerManager>().life);
                break;
        }
    }
}
