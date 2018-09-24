using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShrink : MonoBehaviour
{

    public float respawnTimeMin;
    public float respawnTimeMax;
    private float timeTillRespawn;

    // Use this for initialization
    void Start()
    {
        respawnTimeMin = VariableManager.instance.powerUpShrinkTimeMin;
        respawnTimeMax = VariableManager.instance.powerUpShrinkTimeMax;
        ScheduleRespawn();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Snake")
        {
            Debug.Log("Snake hit the powerup shrink");
            MovementSnake.instance.ShrinkSnake();
            ScheduleRespawn();
        }
    }

    void ScheduleRespawn()
    {
        timeTillRespawn = ((respawnTimeMax - respawnTimeMin) * UnityEngine.Random.value) + respawnTimeMin;
        hideObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVisible)
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
        showObject();
        Vector3 newPosition = Vector3Extensions.getRandomVector();
        newPosition.Scale(VariableManager.instance.mapSize);
        transform.position = newPosition.floorComponentsPlusPoint5();
    }

    private bool isVisible;
    public void MoveToLocation(Vector3 newPosition)
    {
        showObject();
        transform.position = newPosition;
    }

    void hideObject()
    {
        isVisible = false;
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            r.enabled = false;
        }
    }

    void showObject()
    {
        isVisible = true;
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            r.enabled = true;
        }
    }
}
