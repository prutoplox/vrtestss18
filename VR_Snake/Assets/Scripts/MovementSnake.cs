﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSnake : MonoBehaviour {

    public static MovementSnake instance;

    public List<GameObject> snake;
    public GameObject food;
    public GameObject snakeHead;
    public GameObject snakeBodyPart;

    public bool hasWon = false;
    public bool hasLost = false;
    private int msInCurrentStep;
    private int msPerTick;

    private List<Vector3> snakeRotations;
    private Vector3 headTurning;

    // Use this for initialization
    void Start () {

        instance = this;
        msInCurrentStep = 0;
        msPerTick = 500;
        snake = new List<GameObject>();
        snake.Add(snakeHead);
        snake.Add(snakeBodyPart);
        snakeRotations = new List<Vector3>();
        snakeRotations.Add(new Vector3(0, 0, 0));
        snakeRotations.Add(new Vector3(0, 0, 0));
	}

    internal void rotateRight()
    {
        snakeRotations[0] = new Vector3(0, 90, 0);

    }

    internal void rotateLeft()
    {
        snakeRotations[0] = new Vector3(0, -90, 0);
    }

    internal void rotateUp()
    {
        snakeRotations[0] = new Vector3(-90, 0, 0);
    }

    internal void rotateDown()
    {
        snakeRotations[0] = new Vector3(90, 0, 0);
    }

    internal void rotateViewClockwise()
    {
        rotateView(new Vector3(0, 0, 90));
    }
    internal void rotateViewCounterClockwise()
    {
        rotateView(new Vector3(0, 0, -90));
    }

    private void rotateView(Vector3 rotation)
    {
        headTurning += rotation;
        headTurning = modulo(headTurning, new Vector3(360, 360, 360));
        this.transform.Rotate(rotation);
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

    private int isInside(Vector3 toBeChecked, Vector3 cage)
    {
        if (toBeChecked.x < 0 || toBeChecked.x >= cage.x)
        {
            return 1;
        }
        if (toBeChecked.y < 0 || toBeChecked.y >= cage.y)
        {
            return 2;
        }
        if (toBeChecked.z < 0 || toBeChecked.z >= cage.z)
        {
            return 3;
        }
        return 0;
    }
    private Vector3 modulo(Vector3 vectorToBeFitted, Vector3 cage)
    {
        return new Vector3((vectorToBeFitted.x + cage.x) % cage.x, (vectorToBeFitted.y + cage.y) % cage.y, (vectorToBeFitted.z + cage.z) % cage.z);
    }

    private bool isInSameCell(Vector3 a,Vector3 b)
    {
        if (Math.Floor(a.x) != Math.Floor(b.x))
        {
            return false;
        }
        if (Math.Floor(a.y) != Math.Floor(b.y))
        {
            return false;
        }
        if (Math.Floor(a.z) != Math.Floor(b.z))
        {
            return false;
        }
        return true;
    }

    private Vector3 getRandom()
    {
        return new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private Vector3 floorComponents(Vector3 old)
    {
        return new Vector3((float)Math.Floor(old.x), (float)Math.Floor(old.y), (float)Math.Floor(old.z));
    }

    private void UpdatePosition()
    {

        //Für eigentliche Bewegung der Schlange
        for (int i = 0; i < snake.Count; i++)
        {
            //erst Rotation
            snake[i].transform.Rotate(snakeRotations[i]);
            //dann Bewegung 
            snake[i].transform.Translate(Vector3.forward);

            //prüfen, ob der Spielbereich verlassen wurde und ggf loop auf die andere Seite
            checkIfAreaLeftAndFixPosition(snake[i].transform);
        }

        //prüfen, ob das Futter erreicht wurde
        if (isInSameCell(food.transform.position, transform.position))
        {
            MoveFoodToNewLocation();
            GameObject newBody = Instantiate(snakeBodyPart);
            snake.Add(newBody);
            snakeRotations.Add(Vector3.forward);
        }

        snakeRotations[0] += headTurning;
        snakeRotations[0] = modulo(snakeRotations[0], new Vector3(360, 360, 360));
        for (int i = snakeRotations.Count - 1; i > 0; i--)
        {
            snakeRotations[i] = snakeRotations[i - 1];
        }

        //set Rotation of the head to 0
        snakeRotations[0] = new Vector3(0, 0, 0);
        headTurning = new Vector3(0, 0, 0);

    }

    private void MoveFoodToNewLocation()
    {

        //Get a random position
        Vector3 newFoodPostion = getRandom();
        //Make it fit the entire grid
        newFoodPostion.Scale(CreateMap.instance.size);
        //mvoe the food on the edges of the grid
        food.transform.position = floorComponents(newFoodPostion);
        //move it right into the middle of a block in the grid
        food.transform.Translate(new Vector3((float)0.5, (float)0.5, (float)0.5));
    }

    private void checkIfAreaLeftAndFixPosition(Transform toCheck)
    {
        int leftGridVia = isInside(toCheck.position, CreateMap.instance.size);
        if (leftGridVia != 0)
        {
            switch (leftGridVia)
            {
                case 1:
                    Debug.Log("Left the grid on the x axis");
                    if (!CreateMap.instance.isXAxisLooped)
                    {
                        hasLost = true;
                    }
                    break;
                case 2:
                    Debug.Log("Left the grid on the y axis");
                    if (!CreateMap.instance.isYAxisLooped)
                    {
                        hasLost = true;
                    }
                    break;
                case 3:
                    Debug.Log("Left the grid on the z axis");
                    if (!CreateMap.instance.isZAxisLooped)
                    {
                        hasLost = true;
                    }
                    break;
                default:
                    break;
            }
            toCheck.position = modulo(toCheck.position, CreateMap.instance.size);
        }
    }

    // Update is called once per frame
    void Update () {
        UpdatePosition((int)(Time.deltaTime * 1000));
	}
}
