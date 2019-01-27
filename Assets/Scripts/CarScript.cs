﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarScript : MonoBehaviour
{
    public GameObject assembly;

    public float strafeLimit = 40;

    public GameObject camera1;//first view
    public GameObject camera2;//third view

    public Transform planet;
    public Rigidbody rb;

    public float forceAmountForRotation = 10f;
    private Vector3 directionOfPlanetFromCar;
    private bool allowForce;

    public float rotatedSpeed = 4;

    public float power = 0f;

    public Transform steerWheel;
    public Transform powerHandle;
    public GameObject map_2d;
    public AudioSource Ac1;
    public AudioSource Ac2;
    private Quaternion startQ;
    private Quaternion powerQ;
    private bool buttonC = false;

    public GameObject GameOver;

    public GameObject GameWin;

    public GameObject StoryText;
    public GameObject End;
    
    
    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        directionOfPlanetFromCar = Vector3.zero;

        startQ = steerWheel.localRotation;
        powerQ = steerWheel.localRotation;
        StartCoroutine("PlayLoopMusic");
    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            PressRestart();
        }

        Strafe();
        UpdatePower();
    }

    void ChangeToThirdPView()
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
        map_2d.SetActive(false);
    }

    void ChangeToFirstPView()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
        map_2d.SetActive(true);
    }

    public void Strafe()
    {
        float h = Input.GetAxis("Horizontal");



        if (h > 0.1f)
        {
            //print("go right");

            steerWheel.Rotate(Vector3.up, 1f);
            if (assembly.transform.eulerAngles.z < strafeLimit || assembly.transform.eulerAngles.z > 360 - strafeLimit)
            {
                assembly.transform.Rotate(0, 0, -h * Time.fixedDeltaTime * rotatedSpeed, Space.Self);
            }
            transform.Translate(h * Time.fixedDeltaTime * rotatedSpeed, 0, 0, Space.Self);
        }
        else if (h < -0.1f)
        {
            //print("go left");

            steerWheel.Rotate(Vector3.down, 1f);
            if (assembly.transform.eulerAngles.z < strafeLimit || assembly.transform.eulerAngles.z > 360 - strafeLimit)
            {
                assembly.transform.Rotate(0, 0, -h * Time.fixedDeltaTime * rotatedSpeed, Space.Self);
            }
            transform.Translate(h * Time.fixedDeltaTime * rotatedSpeed, 0, 0, Space.Self);
        }
        else
        {
            steerWheel.localRotation = Quaternion.Lerp(steerWheel.localRotation, startQ, Time.deltaTime);

            if (assembly.transform.eulerAngles.z > 1 && assembly.transform.eulerAngles.z <= 170)
                assembly.transform.Rotate(0, 0, -Time.fixedDeltaTime * rotatedSpeed, Space.Self);
            else if (assembly.transform.eulerAngles.z > 182)
                assembly.transform.Rotate(0, 0, Time.fixedDeltaTime * rotatedSpeed, Space.Self);
            transform.Translate(h * Time.fixedDeltaTime * rotatedSpeed, 0, 0, Space.Self);
        }

        //camera1.transform.Rotate(0, 0, -h * Time.fixedDeltaTime * rotatedSpeed, Space.Self);

        //carPoint.transform.Rotate(0, 0, -h * Time.fixedDeltaTime * rotatedSpeed, Space.Self);

    }

    public void UpdatePower()
    {
        float v = Input.GetAxis("Vertical");
        if (v > 0.1f)
        {
            power = power + 0.001f;
        }
        if (v < -0.1f)
        {
            power = power - 0.001f;
        }

        power = Mathf.Clamp(power, 0, 5);
        powerHandle.Rotate(Vector3.right * power, 1f);
        powerHandle.localRotation = Quaternion.Lerp(Quaternion.Euler(powerHandle.localEulerAngles + new Vector3(90/5*power, 0, 0)), powerQ, Time.deltaTime);
        //else
        //{
        //powerHandle.localRotation = Quaternion.Lerp(powerHandle.localRotation, powerQ, Time.deltaTime);
        //}
    }

    void FixedUpdate()
    {
        Vector3 eulerAng = transform.eulerAngles;

        transform.LookAt(planet);
        //if (Input.GetKeyDown(KeyCode.Space))
        //    rb.AddForce(transform.forward * forceAmountForRotation);
        rb.AddForce(transform.forward * power);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AsteroidScript>() != null)
        {
            print("Game Over!");
            GameOver.SetActive(true);
            return;
            // TODO: Add game end.
        }else if(collision.transform.CompareTag("Earth"))
        {
            StartCoroutine("ShowEndResult");         
            return;
        }
    }

    IEnumerator PlayLoopMusic()
    {
        StoryText.SetActive(true);
        yield return new WaitForSeconds(3f);    
        //ToAudio.UpdateVideo(
        //    "Now you are on the way home,use w a s d to move and c for different angel of view.Player two can" +
        //    "use his or her phone to control the turret!");
        //yield return null;
        StoryText.SetActive(false);
        Ac2.Play();
        Ac2.loop = true;
        yield return null;
    }

    public void PressRestart()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator ShowEndResult()
    {
        End.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameWin.SetActive(true);
        yield return null;
    }
}

