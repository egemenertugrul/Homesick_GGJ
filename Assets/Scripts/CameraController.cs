using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up,x);
            transform.Rotate(Vector3.left, y);
        }

        float z = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(z);
        if (z>0.01f||z<-0.01f)
        {
            transform.Translate(Vector3.forward* Time.deltaTime, Space.World);
        }
    }
}
