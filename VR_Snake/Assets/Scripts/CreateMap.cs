using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{

    public Transform startSphere;
    public GameObject sphere;
    public static CreateMap instance;
    public float scalefactor;


    // Use this for initialization
    void Start()
    {
        Vector3 size = VariableManager.instance.mapSize;
        instance = this;
        scalefactor = 1;

        for (int i = 0; i <= size.x; i++)
        {
            for (int j = 0; j <= size.y; j++)
            {
                for (int k = 0; k <= size.z; k++)
                {
                    //Debug.Log(k.ToString() + i.ToString() + j.ToString());
                    //GameObject newGrid = Instantiate(sphere, startSphere.transform.position + new Vector3(i * scalefactor, j * scalefactor, k * scalefactor), startSphere.rotation);
                    GameObject newGrid = Instantiate(sphere, startSphere.transform.position + new Vector3(i * scalefactor, j * scalefactor, k * scalefactor), startSphere.rotation, sphere.transform.parent);
                    newGrid.GetComponent<Renderer>().material.color = new Color(i / size.x, j / size.y, k / size.z);

                    if(i % VariableManager.instance.largerGrid == 0 && j % VariableManager.instance.largerGrid == 0 && k % VariableManager.instance.largerGrid == 0)
                    {
                        newGrid.transform.localScale = VariableManager.instance.largerGridSizeFactor * newGrid.transform.localScale;
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
