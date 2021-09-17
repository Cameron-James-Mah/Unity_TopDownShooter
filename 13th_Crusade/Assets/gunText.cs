using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunText : MonoBehaviour
{
    private GameObject player;
    public Text gunInfoText;
    public int currWeapon;
    public int currMag, currReserves;
    private string gunName = "Rifle";

    // Start is called before the first frame update
    void Start()
    {
        gunInfoText = GetComponent<Text>();
        currWeapon = 1;
        currMag = 30;
        currReserves = 100;
        gunName = "Rifle";
    }

    // Update is called once per frame
    void Update()
    {
        if(currWeapon == 1)
        {
            gunName = "Rifle";
        }
        else if(currWeapon == 2)
        {
            gunName = "Shotgun";
        }
        else if (currWeapon == 3)
        {
            gunName = "Handgun";
            currReserves = 999;
        }
        gunInfoText.text = gunName + "\n" + currMag + "/" + currReserves;
    }
}
