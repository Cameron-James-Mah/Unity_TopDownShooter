using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textScript : MonoBehaviour
{

    //public GameObject player;
    private GameObject player;
    public int playerHp;
    public int legionKilled;
    public Text infoText;
    
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("delayForReference", 0.2f);
        //player = GameObject.Find("Player");
        infoText = GetComponent<Text>();
        //playerHp = player.GetComponent<ShadeEnemy>().health;
        playerHp = 100;
        legionKilled = 0;


    }

    // Update is called once per frame
    void Update()
    {
        /*
        playerHp = player.GetComponent<ShadeEnemy>().health;
        //Debug.Log(playerHp);
        //healthText.text = "Health: ";
        if (player != null)
        {
            healthText.text = "Health: " + playerHp;
        }*/
        infoText.text = "Health: " + playerHp + "\nLegion Killed: " + legionKilled;
    }

    void delayForReference()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHp = player.GetComponent<ShadeEnemy>().health;
    }
}
