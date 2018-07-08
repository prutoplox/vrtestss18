﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSnake : MonoBehaviour {

    public static MovementSnake instance;

    private List<GameObject> snake;
    public GameObject food;
    public GameObject snakeHead;
    public GameObject snakeBodyPart;

    private int msInCurrentStep;
    public bool hasUpdated;
    public bool isPaused;

    private List<Vector3> snakeRotations;
    private Vector3 headTurning;


    public float progressInStep
    {
        get
        {
            if(VariableManager.instance.hasLost || VariableManager.instance.hasWon)
            {
                return 0;
            }
            return (float)msInCurrentStep / VariableManager.instance.msPerMovementOfSnake;
        }
    }

    // Use this for initialization
    void Start()
    {

        instance = this;

        //Check if everything needed is correctly assigned
        if (food == null || snakeHead == null || snakeBodyPart == null)
        {
            throw new MissingReferenceException();
        }

        Reset();

        UpdatePosition();
    }

    public void enterPause()
    {
        isPaused = true;
    }

    public void leavePause()
    {
        isPaused = false;
        msInCurrentStep = 0;
        hasUpdated = false;
    }

    public void togglePause()
    {
        if(isPaused)
        {
            leavePause();
        }
        else
        {
            enterPause();
        }
    }

    public void CollsionWall()
    {
        if (VariableManager.instance.dieOnWallCollsion)
        {
            Debug.Log("Collided with the wall and died");
            VariableManager.instance.hasLost = true;
        }
    }
    public void CollsionBody()
    {
        if (VariableManager.instance.dieOnBodyCollsion)
        {
            Debug.Log("Collided with a part of the body and died");
            VariableManager.instance.hasLost = true;
        }
    }
    public void Reset()
    {
        //Tear down old data if there is some
        if(snake != null)
        {
            /*
             * We don't destroy the first two elements since we still need them to build up a new snake(head and a body "template").
             * All the remaining elements are generated ones which can safely be removed
             */
            for(int i = 2; i < snake.Count; i++)
            {
                Destroy(snake[i]);
            }
        }


        //ignore what was set in the unity editor
        hasUpdated = true;
        isPaused = false;

        //Move the head to the inital postion, offsetting by 0.5f is needed to move it into the middle of a cell, the borders of cells are always at x.0
        snakeHead.transform.position = VariableManager.instance.initalPositionHead + new Vector3(0.5f,0.5f,0.5f);
        snakeHead.transform.rotation = VariableManager.instance.initalRotation;

        snakeBodyPart.transform.position = VariableManager.instance.initalPositionHead + new Vector3(0.5f, 0.5f, 0.5f);
        snakeBodyPart.transform.rotation = VariableManager.instance.initalRotation;
        snakeBodyPart.transform.Translate(Vector3.back);
        Debug.Log("Moved the headto the inital position of " + VariableManager.instance.initalPositionHead.ToString());


        VariableManager.instance.hasWon = false;
        VariableManager.instance.hasLost = false;
        msInCurrentStep = 0;
        snake = new List<GameObject>();
        snake.Add(snakeHead);
        snake.Add(snakeBodyPart);
        snakeRotations = new List<Vector3>();
        snakeRotations.Add(Vector3.zero);
        snakeRotations.Add(Vector3.zero);
        headTurning = Vector3.zero;

        for (int i = 0; i < VariableManager.instance.initalLength; i++)
        {
            GrowSnake();
        }

        MoveFoodToNewLocation();
    }

    internal void rotateRight()
    {
        Debug.Log("Rotating right");
        snakeRotations[0] = new Vector3(0, 90, 0);
    }

    internal void rotateLeft()
    {
        Debug.Log("Rotating left");
        snakeRotations[0] = new Vector3(0, -90, 0);
    }

    internal void rotateUp()
    {
        Debug.Log("Rotating up");
        snakeRotations[0] = new Vector3(-90, 0, 0);
    }

    internal void rotateDown()
    {
        Debug.Log("Rotating down");
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
        headTurning = headTurning.modulo(new Vector3(360, 360, 360));
        this.transform.Rotate(rotation);
    }
    
    public void setRotation(Vector3 newOrientation)
    {
        //TODO make sure the new relative rotation is as wanted
        //Want to set sR[0] so that it rotates the preview orientation to the wanted one.
        snakeRotations[0] = newOrientation - snake[0].transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        hasUpdated = false;
        UpdatePosition((int)(Time.deltaTime * 1000));
    }

    public int UpdatePosition(int msSinceLastCall)
    {
        //Für stabile Updaterate
        int numberTicksDone = 0;
        msInCurrentStep += msSinceLastCall;
        while (msInCurrentStep >= VariableManager.instance.msPerMovementOfSnake)
        {
            UpdatePosition();
            numberTicksDone++;
            msInCurrentStep -= VariableManager.instance.msPerMovementOfSnake;
        }
        return numberTicksDone;
    }

    internal void UpdatePosition()
    {
        if(VariableManager.instance.hasLost || VariableManager.instance.hasWon)
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
            snake[i].transform.position = snake[i].transform.position.floorComponents() + new Vector3(0.5f,0.5f,0.5f);

            //prüfen, ob der Spielbereich verlassen wurde und ggf loop auf die andere Seite
            Vector3 newPosition;
            bool hasLeftArea = snake[i].transform.position.checkIfAreaLeftAndReturnNewPosition(out newPosition);
            snake[i].transform.position = newPosition;


            //lose when the head has left the area
            if (hasLeftArea && i == 0)
            {
                CollsionWall();
            }
        }

        //prüfen, ob das Futter erreicht wurde
        if (food.transform.position.isInSameCell(transform.position))
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
        Vector3 newFoodPostion = Vector3Extensions.getRandomVector();
        //Make it fit the entire grid
        newFoodPostion.Scale(VariableManager.instance.mapSize);
        //mvoe the food on the edges of the grid
        food.transform.position = newFoodPostion.floorComponents();
        //move it right into the middle of a block in the grid
        food.transform.Translate(new Vector3(0.5f, 0.5f, 0.5f));
    }
}
