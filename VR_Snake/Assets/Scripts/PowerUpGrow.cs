﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGrow : MonoBehaviour {
    
    public float respawnTimeMin;
    public float respawnTimeMax;
    private float timeTillRespawn;

	// Use this for initialization
	void Start () {
        respawnTimeMin = VariableManager.instance.powerUpGrowTimeMin;
        respawnTimeMax = VariableManager.instance.powerUpGrowTimeMax;
        ScheduleRespawn();
	}

    void ScheduleRespawn()
    {
        timeTillRespawn = ((respawnTimeMax - respawnTimeMin) * UnityEngine.Random.value) + respawnTimeMin;
        transform.GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!transform.GetComponent<MeshRenderer>().enabled)
        {
            timeTillRespawn -= Time.deltaTime;
            if (timeTillRespawn <= 0)
            {
                SpawnAtNewPosition();
            }
        }
	}

    private void SpawnAtNewPosition()
    {
        transform.GetComponent<MeshRenderer>().enabled = true;
        Vector3 newPosition = Vector3Extensions.getRandomVector();
        newPosition.Scale(VariableManager.instance.mapSize);
        transform.position = newPosition.floorComponentsPlusPoint5();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Snake")
        {
            Debug.Log("Snake hit the powerup shrink");
            MovementSnake.instance.GrowSnake();
            ScheduleRespawn();
        }
    }
}