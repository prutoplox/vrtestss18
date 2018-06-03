using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSnake : MonoBehaviour {

    public List<GameObject> snake;
    public GameObject snakeHead;
    public GameObject snakeBodyPart;
    private int msInCurrentStep;
    private int msPerTick;
    private string nextDir;
    public static MovementSnake instance;
    private Vector3 nextRotation;

    // Use this for initialization
    void Start () {

        instance = this;
        msInCurrentStep = 0;
        msPerTick = 1000;
        snake = new List<GameObject>();
        snake.Add(snakeHead);
	}

    internal void rotateRight()
    {
        nextRotation = new Vector3(0, 0, 90);

    }

    internal void rotateLeft()
    {
        nextRotation = new Vector3(0, 0, -90);
    }

    internal void rotateUp()
    {
        nextRotation = new Vector3(90, 0, 0);
    }

    internal void rotateDown()
    {
        nextRotation = new Vector3(-90, 0, 0);
    }

    public int UpdatePosition(int msSinceLastCall)
    {
        //Für stabile Updaterate
        int numberTicksDone = 0;
        msInCurrentStep += msSinceLastCall;
        while (msInCurrentStep >= msPerTick)
        {
            UpdatePosition();
            numberTicksDone++;
            msInCurrentStep -= msPerTick;
        }
        return numberTicksDone;
    }

    private void UpdatePosition()
    {

        //Für eigentliche Bewegung der Schlange
        //erst Rotation
        
        snake[0].transform.Rotate(nextRotation);
        //dann Bewegung 
        snake[0].transform.Translate(Vector3.forward);
        //set Rotation to 0
        nextRotation = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update () {
        UpdatePosition((int)(Time.deltaTime * 1000));
	}
}
