using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{

    public Transform startSphere;
    public GameObject sphere;
    public Vector3 size = new Vector3(20, 20, 20);
    public bool isXAxisLooped;
    public bool isYAxisLooped;
    public bool isZAxisLooped;
    public static CreateMap instance;
    public float scalefactor;

    public int largerGrid = 5;
    public float largerGridSizeFactor = 5f;

    // Use this for initialization
    void Start()
    {
        instance = this;
        scalefactor = 1;

        for (int i = 0; i <= size.x; i++)
        {
            for (int j = 0; j <= size.y; j++)
            {
                for (int k = 0; k <= size.z; k++)
                {
                    //Debug.Log(k.ToString() + i.ToString() + j.ToString());
                    GameObject newGrid = Instantiate(sphere, startSphere.transform.position + new Vector3(i * scalefactor, j * scalefactor, k * scalefactor), startSphere.rotation);
                    newGrid.GetComponent<Renderer>().material.color = new Color(i / size.x, j / size.y, k / size.z);

                    if(i % largerGrid == 0 && j % largerGrid == 0 && k % largerGrid == 0)
                    {
                        newGrid.transform.localScale = largerGridSizeFactor * newGrid.transform.localScale;
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
