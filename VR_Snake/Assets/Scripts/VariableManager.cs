﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the VariableManger of SnakeVR
public class VariableManager : MonoBehaviour{

    //Instance of the VariableManager (static)
    public static VariableManager instance = new VariableManager();

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
    //MovementSnake(Logic)
    public int msPerMovementOfSnake;
    public bool dieOnBodyCollsion;
    public bool dieOnWallCollsion;
    //SnakeGaze
    public float gazeLength;
    public float correctGazeYPosition;

    public void Start()
    {
        instance = this;

        useExperimental = false;
        useKeyboard = true;
        useVRAdvanced = false;
        useVRBasic = false;
        useVR360persistent = false;
        useVRControllerBasic = false;
        useVRControllerBasicButtons = false;
        useRotationControl = false;


        hasWon = false;
        hasLost = false;
        isXAxisLooped = false;
        isYAxisLooped = false;
        isZAxisLooped = false;
        msPerMovementOfSnake = 500;
        dieOnBodyCollsion = true;
        dieOnWallCollsion = true;
        timeBetweenChangeDirection = 2.0f;
        sensitivityHorizontal = 0.3f;
        sensitivityVertical = 0.1f;
        correctGazeYPosition = 0.25f;
        gazeLength = 30f;
        mapSize = new Vector3(20, 20, 20);
        largerGrid = 5;
        largerGridSizeFactor = 5f;
    }

    //public Methods here
    public static void doSth()
    {
        Debug.Log("do sth");
    }

    
}
