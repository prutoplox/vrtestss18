using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {


    public float respawnTimeMin;
    public float respawnTimeMax;
    private float timeTillRespawn;

    // Use this for initialization
    void Start()
    {
        respawnTimeMin = VariableManager.instance.foodTimeMin;
        respawnTimeMax = VariableManager.instance.foodTimeMax;
        ScheduleRespawn();
    }

    void ScheduleRespawn()
    {
        timeTillRespawn = ((respawnTimeMax - respawnTimeMin) * UnityEngine.Random.value) + respawnTimeMin;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
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
        Vector3 newPosition = Vector3Extensions.getRandomVector();
        newPosition.Scale(VariableManager.instance.mapSize);
        MoveToLocation(newPosition.floorComponentsPlusPoint5());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Snake")
        {
            Debug.Log("Snake hit some food");
            ScheduleRespawn();
            MovementSnake.instance.GrowSnake();
            AudioManager.instance.playSnakeEat();
            VariableManager.instance.eatPoints();
        }
    }

    public void MoveToLocation(Vector3 newPosition)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }
        transform.position = newPosition;
    }
}
