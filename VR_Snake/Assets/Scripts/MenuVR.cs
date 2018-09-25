using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVR : MonoBehaviour
{
    public GameObject[] buttons;
    int selectedButton;
    float scaleFactor = 2f;
    Vector3 originalScale;

    // Use this for initialization
    void Start()
    {
        
        selectedButton = 0;
        enableButton(selectedButton);
    }

    public void selectMenu()
    {
        disableButton(selectedButton);
        selectedButton = 0;
        enableButton(selectedButton);
    }

    // Update is called once per frame
    void Update()
    {
        Canvas parentcanvas = GetComponent<Canvas>();
        if (!parentcanvas.enabled)
        {
            return;
        }

        //if (Input.GetButtonDown("menudown"))
        if (Input.GetKeyUp(KeyCode.M))
        {
            selectNextButton();
        }

        //if (Input.GetButtonDown("menuup"))
        if (Input.GetKeyUp(KeyCode.N))
        {
            selectPreviousButton();
        }

        //if (Input.GetButtonDown("menuup"))
        if (Input.GetKeyUp(KeyCode.B))
        {
            selectedMenuFromButton();
        }
    }

    private void selectPreviousButton()
    {
        disableButton(selectedButton);
        selectedButton = ((selectedButton + buttons.Length) - 1) % buttons.Length;
        enableButton(selectedButton);
    }

    private void selectNextButton()
    {
        disableButton(selectedButton);
        selectedButton = (selectedButton + 1) % buttons.Length;
        enableButton(selectedButton);
    }

    private void selectedMenuFromButton()
    {
        if (buttons[selectedButton].name == "StartGame")
        {
            StartGame.startThisGame = true;
            VariableManager.instance.startGame = true;
            //VariableManager.instance.startTime = Time.time;
            VariableManager.instance.showNoMenu();
            VariableManager.instance.useGameCam();
            if (SnakeAutopilot.instance.isActive)
            {
                SnakeAutopilot.instance.deactivateAutopilot();
            }
        }
        else if (buttons[selectedButton].name == "MainMenu")
        {
            VariableManager.instance.useMenuCam();
            VariableManager.instance.showMainMenuMenu();
        }
        else if (buttons[selectedButton].name == "Highscore")
        {
            VariableManager.instance.useMenuCam();
            VariableManager.instance.showHighscoresText();
            VariableManager.instance.showHighscoreMenu();
        }
        else if (buttons[selectedButton].name == "Options")
        {
            VariableManager.instance.useMenuCam();
            VariableManager.instance.showOptionsMenu();
        }
        else if (buttons[selectedButton].name == "Resume")
        {
            VariableManager.instance.useMenuCam();
            VariableManager.instance.showNoMenu();
        }
        else if (buttons[selectedButton].name == "Exit")
        {
            Debug.Log("APP QUIT");
            Application.Quit();

            //Not reached once deployed
            return;
        }
        else
        {
            Debug.Log("Menubutton not identified");
            Debug.Log(buttons[selectedButton].name);
            throw new NotImplementedException();
        }
    }

    private void enableButton(int buttonID)
    {
        originalScale = buttons[buttonID].transform.GetChild(0).localScale;
        buttons[buttonID].transform.GetChild(0).localScale = buttons[buttonID].transform.GetChild(0).localScale * scaleFactor;
    }

    private void disableButton(int buttonID)
    {
        buttons[buttonID].transform.GetChild(0).localScale = originalScale;
    }
}
