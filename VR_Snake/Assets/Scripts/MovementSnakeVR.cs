﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MovementSnakeVR : MonoBehaviour {

    public static MovementSnakeVR instance;

    
    private List<GameObject> snake;
    public GameObject food;
    public GameObject snakeHead;
    public GameObject snakeBodyPart;

    public bool hasWon;
    public bool hasLost;
    private int msInCurrentStep;
    public int msPerTick;
    public float progressInStep;
    public bool hasUpdated = false;

    private List<Vector3> snakeRotations;
    private Vector3 headTurning;

    // Use this for initialization
    void Start () {
        
        instance = this;
        
        //Check if everything needed is correctly assigned
        if (food == null || snakeHead == null || snakeBodyPart == null)
        {
            throw new MissingReferenceException();
        }

        Reset();

        //Testing
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
	}

    public void Reset()
    {
        hasWon = false;
        hasLost = false;

        msInCurrentStep = 0;
        snake = new List<GameObject>();
        snake.Add(snakeHead);
        snake.Add(snakeBodyPart);
        snakeRotations = new List<Vector3>();
        snakeRotations.Add(Vector3.zero);
        snakeRotations.Add(Vector3.zero);

        headTurning = Vector3.zero;

        MoveFoodToNewLocation();
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

    // Update is called once per frame
    void Update()
    {
        hasUpdated = false;
        UpdatePosition((int)(Time.deltaTime * 1000));
        progressInStep = (float)msInCurrentStep / msPerTick;
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
        if(hasLost || hasWon)
        {
            Debug.Log("Trying to move while lost/won");
            return;
        }

        hasUpdated = true;

        //Make the body follow the new rotation of the head, without that the tail might get detached from the head
        snakeRotations[1] += headTurning;

        //Für eigentliche Bewegung der Schlange
        for (int i = 0; i < snake.Count; i++)
        {
            //erst Rotation
            snake[i].transform.Rotate(snakeRotations[i]);
            //dann Bewegung 
            snake[i].transform.Translate(Vector3.forward);
            snake[i].transform.position = floorComponents(snake[i].transform.position) + new Vector3(0.5f,0.5f,0.5f);
            
            //prüfen, ob der Spielbereich verlassen wurde und ggf loop auf die andere Seite
            bool hasLeftArea = checkIfAreaLeftAndFixPosition(snake[i].transform);
            
            //lose when the head has left the area
            if(hasLeftArea && i == 0)
            {
                hasLost = true;
            }
        }

        //prüfen, ob das Futter erreicht wurde
        if (isInSameCell(food.transform.position, transform.position))
        {
            MoveFoodToNewLocation();
            GrowSnake();
        }

        for (int i = snakeRotations.Count - 1; i > 0; i--)
        {
            snakeRotations[i] = snakeRotations[i - 1];
        }

        //set Rotation of the head to 0
        snakeRotations[0] = Vector3.zero;
        headTurning = Vector3.zero;
    }

    private void GrowSnake()
    {
        GameObject newBody = Instantiate(snake[snake.Count - 1]);
        snake.Add(newBody);
        newBody.transform.Translate(Vector3.back);
        newBody.transform.Rotate(new Vector3(360, 360, 360) - snakeRotations[snakeRotations.Count - 1]);
        snakeRotations.Add(Vector3.forward);
    }

    public int GetLength()
    {
        return snake.Count - 1;
    }

    public GameObject GetSnakePartGameObject(int index)
    {
        if (index == snake.Count - 1)
            throw new IndexOutOfRangeException();

        return snake[index];
    }

    public Transform GetStartPositionOf(int index)
    {
        return GetSnakePartGameObject(index).transform;
    }

    public Transform GetEndPositionOf(int index)
    {
        index++;
        if (index == 0)
            throw new IndexOutOfRangeException();
        return snake[index].transform;
    }

    private void MoveFoodToNewLocation()
    {

        //Get a random position
        Vector3 newFoodPostion = getRandomVector();
        //Make it fit the entire grid
        newFoodPostion.Scale(CreateMap.instance.size);
        //mvoe the food on the edges of the grid
        food.transform.position = floorComponents(newFoodPostion);
        //move it right into the middle of a block in the grid
        food.transform.Translate(new Vector3(0.5f, 0.5f, 0.5f));
    }

    private Vector3 getRandomVector()
    {
        return new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private Vector3 floorComponents(Vector3 old)
    {
        return new Vector3((float)Math.Floor(old.x), (float)Math.Floor(old.y), (float)Math.Floor(old.z));
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

    private bool isInSameCell(Vector3 a, Vector3 b)
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

    private bool checkIfAreaLeftAndFixPosition(Transform toCheck)
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
                        return true;
                    }
                    break;
                case 2:
                    Debug.Log("Left the grid on the y axis");
                    if (!CreateMap.instance.isYAxisLooped)
                    {
                        return true;
                    }
                    break;
                case 3:
                    Debug.Log("Left the grid on the z axis");
                    if (!CreateMap.instance.isZAxisLooped)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
            toCheck.position = modulo(toCheck.position, CreateMap.instance.size);
        }
        return false;
    }
}
