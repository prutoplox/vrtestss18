﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSnake : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("up");
            MovementSnake.instance.rotateUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("down");
            MovementSnake.instance.rotateDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("left");
            MovementSnake.instance.rotateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right");
            MovementSnake.instance.rotateRight();
        }
    }
}
