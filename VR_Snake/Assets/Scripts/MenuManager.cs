using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject highscore;
    public GameObject options;
    public GameObject pause;
    public GameObject gameover;

    //cams
    public Camera menucam;

    public Camera maincam;

    private void Start()
    {
        Debug.Log("MenuManager Init");
        useMenuCam();
    }

    // Update is called once per frame
    void Update()
    {
        if (VariableManager.instance.startGame)
        {
            useGameCam();
        }

        if (VariableManager.instance.showMainMenu == true)
        {
            useMenuCam();
            mainmenu.GetComponent<Canvas>().enabled = true;

            if (AudioManager.instance.sourceMain.clip != AudioManager.instance.menuMusic)
            {
                AudioManager.instance.playMenuMusic();
            }
        }
        else
        {
            mainmenu.GetComponent<Canvas>().enabled = false;
        }

        if (VariableManager.instance.showHighscore == true)
        {
            useMenuCam();

            highscore.GetComponent<Canvas>().enabled = true;

            if (AudioManager.instance.sourceMain.clip != AudioManager.instance.highScoreMusic)
            {
                AudioManager.instance.playHighscoreMusic();
            }
        }
        else
        {
            highscore.GetComponent<Canvas>().enabled = false;
        }

        if (VariableManager.instance.showGameOver == true)
        {
            useMenuCam();
            gameover.GetComponent<Canvas>().enabled = true;

            if (AudioManager.instance.sourceMain.clip != AudioManager.instance.gameOverMusic)
            {
                AudioManager.instance.playGameOverMusic();
            }
        }
        else
        {
            gameover.GetComponent<Canvas>().enabled = false;
        }

        if (VariableManager.instance.showOptions == true)
        {
            useMenuCam();
            options.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            options.GetComponent<Canvas>().enabled = false;
        }

        if (VariableManager.instance.showPause == true)
        {
            useMenuCam();
            pause.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            pause.GetComponent<Canvas>().enabled = false;
        }
    }

    private void useGameCam()
    {
        menucam.enabled = false;
        maincam.enabled = true;
        if (SnakeAutopilot.instance.isActive)
        {
            SnakeAutopilot.instance.deactivateAutopilot();
        }
    }

    private void useMenuCam()
    {
        maincam.enabled = false;
        menucam.enabled = true;
        if (!SnakeAutopilot.instance.isActive)
        {
            SnakeAutopilot.instance.activateAutopilot();
        }
    }
}
