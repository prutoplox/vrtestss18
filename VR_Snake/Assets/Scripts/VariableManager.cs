using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the VariableManger of SnakeVR
public class VariableManager : MonoBehaviour
{
    //Instance of the VariableManager (static)
    public static VariableManager instance = new VariableManager();

    public HighScoreManager highScoreManager;

    public bool isDebug;

    //Controller variables
    public bool useVRBasic;

    public bool useVR360persistent;
    public bool useKeyboard;
    public bool useVRAdvanced;
    public bool useExperimental;
    public bool useVRControllerBasic;
    public bool useVRControllerBasicButtons;
    public bool useRotationControl;

    //Win/Lose conditions
    public bool hasWon;

    public bool hasLost;

    //CreateMap
    public Vector3 mapSize;

    public int largerGrid;
    public float largerGridSizeFactor;
    public bool isXAxisLooped;
    public bool isYAxisLooped;
    public bool isZAxisLooped;
    public float scalefactor;

    //MovementSnakeVR
    public float timeBetweenChangeDirection;

    public float sensitivityHorizontal;
    public float sensitivityVertical;

    //Food
    public float foodTimeMin;

    public float foodTimeMax;

    //MovementSnake(Logic)
    public int msPerMovementOfSnake;

    public bool dieOnBodyCollsion;
    public bool dieOnWallCollsion;
    public int initalLength;
    public Vector3 initalPositionHead;
    public Quaternion initalRotation;
    public bool placeRandomFoodActive;

    //Snake 3D Object
    public float snakeThicknessX;

    public float snakeThicknessZ;

    //SnakeGaze
    public float gazeLength;

    public float correctGazeYPosition;

    //User Interface/Menu
    public bool startGame;

    //
    public bool showMainMenu;

    public bool showHighscore;
    public bool showGameOver;
    public bool showOptions;
    public bool showPause;

    //Score
    public float timeScore;

    public float score;
    public float bonusScore;
    public float startTime;
    public float allTime;
    public float gameTime;

    //
    public float place1;

    public float place2;
    public float place3;
    public float place4;
    public float place5;

    //Power Ups
    //Shrink
    public float powerUpShrinkTimeMin;

    public float powerUpShrinkTimeMax;

    //Grow
    public float powerUpGrowTimeMin;
    public float powerUpGrowTimeMax;
    //golden Apple
    public float goldenAppleTimeMin;
    public float goldenAppleTimeMax;
    public float goldenApplePointMultiplier;
    //mulit apple    
    public float multiAppleTimeMin;
    public float multiAppleTimeMax;
    public float multiAppleExtraApples;
    //row apple
    public float rowAppleTimeMin;
    public float rowAppleTimeMax;
    public float rowAppleExtraApples;

    public float powerUpGrowTimeMax;

    /*
     * Force that the correct values are accessible each and every time
     * and not depending on the order when the start() was called.
     */

    public void Awake()
    {
        getHighScores();
        instance = this;
        isDebug = true;
        useExperimental = false;
        useKeyboard = true;
        useVRAdvanced = false;
        useVRBasic = false;
        useVR360persistent = false;
        useVRControllerBasic = false;
        useVRControllerBasicButtons = false;
        useRotationControl = false;

        timeBetweenChangeDirection = 2.0f;
        sensitivityHorizontal = 0.3f;
        sensitivityVertical = 0.1f;
        correctGazeYPosition = 0.25f;
        gazeLength = 30f;

        foodTimeMin = 1;
        foodTimeMax = 2;

        mapSize = new Vector3(20, 20, 20);
        largerGrid = 5;
        largerGridSizeFactor = 5f;
        isXAxisLooped = false;
        isYAxisLooped = false;
        isZAxisLooped = false;

        hasWon = false;
        hasLost = false;
        msPerMovementOfSnake = 500;
        dieOnBodyCollsion = true;
        dieOnWallCollsion = true;
        initalLength = 2;
        initalPositionHead = new Vector3(mapSize.x / 2, mapSize.y / 2, 0);
        initalRotation = Quaternion.Euler(Vector3.zero);
        placeRandomFoodActive = true;

        snakeThicknessX = 0.6f;
        snakeThicknessZ = snakeThicknessX;

        startGame = false;
        showMainMenu = true;
        showHighscore = false;
        showGameOver = false;
        showOptions = false;
        showPause = false;

    //Shrink
      powerUpShrinkTimeMin = 40;
      powerUpShrinkTimeMax = 45;
    //Grow
      powerUpGrowTimeMin = 50;
      powerUpGrowTimeMax = 60;
    //golden Apple
      goldenAppleTimeMin = 20;
      goldenAppleTimeMax = 30;
      goldenApplePointMultiplier = 10;
    //mulit apple    
      multiAppleTimeMin = 20;
      multiAppleTimeMax = 60;
      multiAppleExtraApples = 5;
    //row apple
      rowAppleTimeMin = 40;
      rowAppleTimeMax = 120;
      rowAppleExtraApples = 3;
    }

    public void getHighScores()
    {
        if (PlayerPrefs.HasKey("1"))
        {
            place1 = PlayerPrefs.GetFloat("1");
        }
        if (PlayerPrefs.HasKey("2"))
        {
            place2 = PlayerPrefs.GetFloat("2");
        }
        if (PlayerPrefs.HasKey("3"))
        {
            place3 = PlayerPrefs.GetFloat("3");
        }
        if (PlayerPrefs.HasKey("4"))
        {
            place4 = PlayerPrefs.GetFloat("4");
        }
        if (PlayerPrefs.HasKey("5"))
        {
            place5 = PlayerPrefs.GetFloat("5");
        }
    }

    //public Methods here
    public static void doSth()
    {
        Debug.Log("do sth");
    }

    public void eatPoints()
    {
        bonusScore += 100;
    }
}
