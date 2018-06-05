using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Body")
        {
            Debug.Log("Bodycollision :" + other.ToString());
        }
        else if (other.gameObject.tag == "Level")
        {
            Debug.Log("Levelcollision :" + other.ToString());
        }
        else if (other.gameObject.tag == "Food")
        {
            Debug.Log("Foodcollision :" + other.ToString());
        }
        else
        {
            Debug.Log("Other collision :" + other.ToString());
        }
    }
   
}
