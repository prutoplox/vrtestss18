using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour{

    public static VariableManager instance = new VariableManager();
    //public Variables here
    //Controller Variables
    public bool useVRBasic;
    public bool useVR360persistent;
    public bool useKeyboard;
    public bool useVRAdvanced;
    public bool useExperimental;
    public bool hasWon;
    public bool hasLost;

    public Vector3 mapSize = new Vector3(20, 20, 20);
    public int largerGrid = 5;
    public float largerGridSizeFactor = 5f;

    public bool isXAxisLooped;
    public bool isYAxisLooped;
    public bool isZAxisLooped;


    public void Start()
    {
        instance = this;
        useExperimental = false;
        useKeyboard = true;
        useVRAdvanced = false;
        useVRBasic = false;
        useVR360persistent = false;
        hasWon = false;
        hasLost = false;
        isXAxisLooped = true;
        isYAxisLooped = true;
        isZAxisLooped = true;
    }

    //public Methods here
    public static void doSth()
    {
        Debug.Log("do sth");
    }

    
}
