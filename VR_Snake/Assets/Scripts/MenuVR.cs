﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVR : MonoBehaviour
{
    public GameObject[] buttons;
    int selectedButton;
    float scaleFactor = 1.3f;
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
        //if (Input.GetButtonDown("menudown"))
        if (Input.GetKeyDown(KeyCode.M))
        {
            selectNextButton();
        }

        //if (Input.GetButtonDown("menuup"))
        if (Input.GetKeyDown(KeyCode.N))
        {
            selectPreviousButton();
        }

        //if (Input.GetButtonDown("menuup"))
        if (Input.GetKeyDown(KeyCode.B))
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
            VariableManager.instance.showNoMenu();
        }
        if (buttons[selectedButton].name == "Zurück zum Menü")
        {
            VariableManager.instance.showMainMenuMenu();
        }
        if (buttons[selectedButton].name == "Highscore")
        {
            VariableManager.instance.showHighscoreMenu();
        }
        if (buttons[selectedButton].name == "RestartGame")
        {
            VariableManager.instance.showNoMenu();
        }
        if (buttons[selectedButton].name == "Options")
        {
            VariableManager.instance.showOptionsMenu();
        }
        if (buttons[selectedButton].name == "Resume")
        {
            VariableManager.instance.showNoMenu();
        }
        if (buttons[selectedButton].name == "Exit")
        {
            Debug.Log("APP QUIT");
            Application.Quit();
        }

        throw new NotImplementedException();
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