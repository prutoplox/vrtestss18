using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

    public Transform startSphere;
    public GameObject sphere;
    public Vector3 size = new Vector3(20, 20, 20);
    public bool isXAxisLooped = true;
    public bool isYAxisLooped = true;
    public bool isZAxisLooped = true;
    public static CreateMap instance;
    public float scalefactor;

	// Use this for initialization
	void Start () {
        instance = this;
        scalefactor = 1;

        for (int i = 0; i <= size.x; i++)
        {
            for (int j = 0; j <= size.y; j++)
            {
                for(int k = 0; k <= size.z; k++)
                {
                    //Debug.Log(k.ToString() + i.ToString() + j.ToString());
                    Instantiate(sphere, startSphere.transform.position + new Vector3(i*scalefactor,j*scalefactor,k*scalefactor) , startSphere.rotation);
                }   
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
