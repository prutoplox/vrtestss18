using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour{

    public static VariableManager instance = new VariableManager();
    //public Variables here
    //Controller Variables
    public bool useVRBasic;
    public bool useKeyboard;
    public bool useVRAdvanced;
    public bool useExperimental;
    public bool hasWon;
    public bool hasLost;


    public void Start()
    {
        instance = this;
        useExperimental = false;
        useKeyboard = true;
        useVRAdvanced = false;
        useVRBasic = false;
        hasWon = false;
        hasLost = false;
    }

    //public Methods here
    public static void doSth()
    {
        Debug.Log("do sth");
    }

    
}
