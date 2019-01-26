using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Transform farView;
    public Transform nearview;

    public Transform mainCam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            mainCam.SetParent(nearview);
            mainCam.localPosition = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            mainCam.SetParent(farView);
            mainCam.localPosition = Vector3.zero;
        }
    }
}
