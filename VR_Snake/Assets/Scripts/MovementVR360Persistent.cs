﻿using UnityEngine;
using UnityEngine.XR;

//Script for alternative movement
public class MovementVR360Persistent : MonoBehaviour
{
    Vector3 previousRotation = new Vector3(0, 0, 0);

    void Update()
    {
        if (VariableManager.instance.useVR360persistent)
        {
            Debug.Log((InputTracking.GetLocalRotation(XRNode.Head)).ToString());
            changeDirection();
        }
    }

    private void changeDirection()
    {
        Quaternion rotation = InputTracking.GetLocalRotation(XRNode.Head);
        float roundedX = roundToNextAxis(rotation.eulerAngles.x * 360);
        float roundedY = roundToNextAxis(rotation.eulerAngles.y * 360);
        float roundedZ = roundToNextAxis(rotation.eulerAngles.z * 360);
        Vector3 newRotation = new Vector3(roundedX, roundedY, roundedZ);
        if (previousRotation == newRotation)
        {
            Debug.Log("No significant rotation change detected...");
        }
        else
        {
            Debug.Log("Rotating to...");
            Debug.Log(newRotation);
            MovementSnake.instance.setRotation(newRotation);
        }
    }

    private static float roundToNextAxis(float p)
    {
        p = (p % 360 + 360) % 360; //Make sure the value is between 0 and 360
        float pOffset45 = p + 45;
        float tmp2 = pOffset45 / 90;
        float tmp3 = Mathf.Floor(tmp2);
        float returnValue = (tmp3 * 90) % 360;
        return returnValue;
    }
}
