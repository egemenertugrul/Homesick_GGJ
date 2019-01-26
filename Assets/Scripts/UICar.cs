using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICar : MonoBehaviour
{

    public Transform steerWheel;
    public Transform power;

    private Quaternion startQ;
    private Quaternion powerQ;
    // Use this for initialization
    void Start()
    {
        startQ = steerWheel.localRotation;
        powerQ = steerWheel.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWheel();
        UpdatePower();
    }

    public void UpdateWheel()
    {
        float h = Input.GetAxis("Horizontal");
        if (h>0.1f)
        {
            steerWheel.Rotate(Vector3.up, 1f);
        }
        else if (h<-0.1f)
        {
            steerWheel.Rotate(Vector3.down, 1f);
        }
        else
        {
            steerWheel.localRotation = Quaternion.Lerp(steerWheel.localRotation, startQ,Time.deltaTime);
        }
    }

    public void UpdatePower()
    {
        float v = Input.GetAxis("Vertical");
        if (v > 0.1f)
        {
            power.Rotate(Vector3.left, 1f);
        }
        else if (v < -0.1f)
        {
            power.Rotate(Vector3.right, 1f);
        }
        else
        {
            power.localRotation = Quaternion.Lerp(power.localRotation, powerQ, Time.deltaTime);
        }
    }
}
