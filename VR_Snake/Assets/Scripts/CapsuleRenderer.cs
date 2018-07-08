using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleRenderer : MonoBehaviour {

        /*
         * 
            line.positionCount = currentPos.Length;
            line.SetPositions(currentPos);
         */
    private Vector3[] positions = null;

    private GameObject initalCapsule;

    private int positionCount = 0;

    List<GameObject> capsules = new List<GameObject>();


    public int PositionCount
    {
        get
        {
            return positionCount;
        }
        set
        {
            if(value < 0)
            {
                Debug.LogWarning("Must be at least of positive length");
                return;
            }

            // Set the unneeded capsules invisible
            if(value < capsules.Count)
            {
                for(int i= value; i < capsules.Count; i++)
                {
                    capsules[i].GetComponent<MeshRenderer>().enabled = false;
                }
            }
            // Set all in range visible and expand if needed
            else if (value > capsules.Count)
            {
                for (int i = 0; i < capsules.Count; i++)
                {
                    capsules[i].GetComponent<MeshRenderer>().enabled = true;
                }
                for (int i = capsules.Count; i < value; i++)
                {
                    GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    capsules.Add(capsule);
                    capsule.transform.localScale = new Vector3(0.9f, 1, 0.9f); 
                }
            }

            positionCount = value;
        }
    }

    public void SetPositions(Vector3[] positions)
    {
        if(positions.Length != positionCount)
        {
            Debug.LogWarning("The length does not match, PositionCount should be set before this method is called!");
            return;
        }

        for(int i = 0; i < positionCount - 1;i++ )
        {
            SetPosition(capsules[i], positions[i], positions[i + 1]);
        }

    }

    private void SetPosition(GameObject capsule, Vector3 start, Vector3 end)
    {
        //Debug.Log("setting " + capsule + " from " + start + " to " + end);
        //Move the center to the correct location
        capsule.transform.position = (start + end) / 2;

        //Make it the right length
        Vector3 distance = end - start;
        capsule.transform.localScale = new Vector3(capsule.transform.localScale.x, distance.magnitude * 0.9f, capsule.transform.localScale.z); 

        //rotate it into place
        capsule.transform.LookAt(start);
        capsule.transform.Rotate(new Vector3(90, 0, 0));
    }
}
