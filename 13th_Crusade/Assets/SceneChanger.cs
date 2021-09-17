using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    // Update is called once per frame
    private GameObject text;
    //private bool isReading = false;
    //private bool victory = false;



    void Start()
    {
        animator = GetComponent<Animator>();
        text = GameObject.FindGameObjectWithTag("StartText");
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            StateNameController.difficulty = "NA";
            StateNameController.result = "NA";
        }

    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1 && StateNameController.result == "Victory")
        {
            FadeToLevel(2);
            //Debug.Log("Victory");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1 && StateNameController.result == "Defeat")
        {
            FadeToLevel(2);
            //Debug.Log("Defeat");
        }
        /*
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.GetMouseButton(0))
            {
                FadeToLevel(1);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //If victory then load victory screen, if defeat load defeat screen
        }*/
    }
        /*
        public void IntroText()
        {
            isReading = true;
            text.GetComponent<UnityEngine.UI.Text>().text += "Introduction:" +
                "\nIt has been many years since the war between humanity and the legion began. The war has claimed and unfathomable amount of human lives and many more will die" +
                " unless we end this war. You are the Commander of the Hamelin organization's 13th Crusade sent to eliminate their leader Red eye and any legion that get in your way." +
                "\n\n\n\n Objective:" +
                "\n-Elimination of red eye" +
                "\n-Elimination of nearby legion forces";

        }*/

    public void FadeToLevel(int levelIndex)
        {
            levelToLoad = levelIndex;
            animator.SetTrigger("FadeOut");
        }

    public void OnFadeComplete()
        {
            SceneManager.LoadScene(levelToLoad);
        }

    public void Replay()
    {
        //SceneManager.LoadScene("StartScreen");
        FadeToLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EasySelect()
    {
        StateNameController.difficulty = "Easy";
    }
    public void MediumSelect()
    {
        StateNameController.difficulty = "Normal";
    }
    public void HardSelect()
    {
        StateNameController.difficulty = "Hard";
    }

    public void StartPlay()
    {
        if(StateNameController.difficulty == "NA")
        {
            //Prompt user to select diffuculty
        }
        else
        {
            StateNameController.legionKillCounter = 0;
            FadeToLevel(1);
        }
    }






}
