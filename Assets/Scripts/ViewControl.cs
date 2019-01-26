using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour
{
    public GameObject camera1;//first view
    public GameObject camera2;//third view
    public GameObject carPoint;
    bool buttonC = false;

    public float rotatedSpeed = 4;

    public float moveSpeed = 5;
	// Use this for initialization
	void Start () {
		
	}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!buttonC)
            {
                ChangeToThirdPView();
                buttonC = true;
            }
            else
            {
                ChangeToFirstPView();
                buttonC = false;
            }
        }

       

    }
    // Update is called once per frame
    void FixedUpdate () {
	    
	    if (((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D))))
	    {
	        Rotate();       
	    }

        carPoint.transform.Translate(Vector3.forward * Time.fixedDeltaTime * moveSpeed);
    }

    void Rotate()
    {
        float h = Input.GetAxis("Horizontal");
        camera1.transform.Rotate(0,0,-h*Time.fixedDeltaTime*rotatedSpeed,Space.Self);
        carPoint.transform.Rotate(0, 0, -h * Time.fixedDeltaTime * rotatedSpeed, Space.Self);
    }

    void ChangeToThirdPView()
    {
        camera1.SetActive(false);
        camera2.SetActive(true);   
    }

    void ChangeToFirstPView()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }
}
