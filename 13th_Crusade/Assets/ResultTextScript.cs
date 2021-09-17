using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextScript : MonoBehaviour
{
    private string difficultyText, result, victoryText, defeatText;
    private int legionKilledText;
    private float timeSurvivied;
    public Text infoText;
    // Start is called before the first frame update
    void Start()
    {
        infoText = GetComponent<Text>();
        difficultyText = StateNameController.difficulty;
        legionKilledText = StateNameController.legionKillCounter;
        timeSurvivied = StateNameController.time;
        result = StateNameController.result;
        victoryText = "You eliminated Red Eye. Unfortunately you died from injuries sustained in the battle. Project Gestalt continued...";
        defeatText = "Red Eye escaped and continued the war, humanity perished before Project Gestalt could be implemented...";
    }

    // Update is called once per frame
    void Update()
    {
        if (result == "Victory")
        {
            infoText.text = "\t\t\t\t\t\t\t\t\t\t\t" + result + "\n\n\n" + victoryText + "\n\n\n\n\n\nDifficulty: " + difficultyText + "\nLegion Killed: " + legionKilledText + "\nTime survived: " + timeSurvivied + "s";
        }
        else if(result == "Defeat")
        {
            infoText.text = "\t\t\t\t\t\t\t\t\t\t\t" + result + "\n\n\n" + defeatText + "\n\n\n\n\n\nDifficulty: " + difficultyText + "\nLegion Killed: " + legionKilledText + "\nTime survived: " + timeSurvivied + "s";
        }
    }
}
