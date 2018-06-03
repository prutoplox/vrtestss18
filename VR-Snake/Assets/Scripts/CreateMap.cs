using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

    public Transform startSphere;
    public GameObject sphere;
    public int size;
    public float scalefactor;

	// Use this for initialization
	void Start () {
        scalefactor = 1;

        for (int i = 0; i <= size; i++)
        {
            for (int j = 0; j <= size; j++)
            {
                for(int k = 0; k <= size; k++)
                {
                    //Debug.Log(k.ToString() + i.ToString() + j.ToString());
                    Instantiate(sphere, startSphere.transform.position + new Vector3(k*scalefactor,j*scalefactor,i*scalefactor) , startSphere.rotation);
                }   
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
