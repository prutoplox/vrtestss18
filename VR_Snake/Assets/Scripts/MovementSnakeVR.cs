using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.WSA;

public class MovementSnakeVR : MonoBehaviour
{
    const float moveRate = 2.0f;
    float timeLeft =  moveRate;

    public void Update()
    {
        if (VariableManager.instance.useVRBasic)
        {
            Debug.Log((InputTracking.GetLocalRotation(XRNode.Head)).ToString());
            if (timeLeft <= 0)
            {
                changeDirection();
                timeLeft = moveRate;
            }
            timeLeft -= Time.deltaTime;
        }
    }

    private void changeDirection()
    {
        if (InputTracking.GetLocalRotation(XRNode.Head).x > 0.1)
        {
            MovementSnake.instance.rotateDown();
        }
        else if (InputTracking.GetLocalRotation(XRNode.Head).x < -0.1)
        {
            MovementSnake.instance.rotateUp();
        }
        if (InputTracking.GetLocalRotation(XRNode.Head).y > 0.3)
        {
            MovementSnake.instance.rotateRight();
        }
        else if (InputTracking.GetLocalRotation(XRNode.Head).y < -0.3)
        {
            
            MovementSnake.instance.rotateLeft();
        }
        /* TODO Use Controller because of %§$$%%&
        if (InputTracking.GetLocalRotation(XRNode.Head).z > 0.05)
        {
            MovementSnake.instance.rotateViewCounterClockwise();
        }
        else if (InputTracking.GetLocalRotation(XRNode.Head).z < -0.05)
        {
            MovementSnake.instance.rotateViewClockwise();
        }
        */
    }

    public void Start()
    {
        
    }
}
