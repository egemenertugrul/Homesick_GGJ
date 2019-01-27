using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlayer : MonoBehaviour
{

    public Camera cam;
    public GameObject turretModel;
    public Vector2 lastFire;
    public bool lastFired = true;
    public int id;

    public GameObject rocket;
    public float turretCooldown = 0.3f;
    private bool canFire = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!lastFired)
        {
            lastFire = new Vector2(cam.pixelWidth * lastFire.x, cam.pixelHeight * lastFire.y);
            turretModel.transform.LookAt(cam.ScreenPointToRay(lastFire).direction * RocketScript.defaultRocketSpeed);
            //Debug.DrawRay(cam.transform.position, cam.ScreenPointToRay(lastFire).direction, Color.white, 1f);

            lastFired = true;
            if (canFire)
            {
                GameObject newRocket = Instantiate(rocket);
                newRocket.transform.position = turretModel.transform.position;
                newRocket.transform.eulerAngles = new Vector3(turretModel.transform.eulerAngles.x, turretModel.transform.eulerAngles.y, turretModel.transform.eulerAngles.z);
                newRocket.GetComponent<Rigidbody>().AddForce(newRocket.transform.forward * RocketScript.defaultRocketSpeed);
                Destroy(newRocket, RocketScript.lifetime);
                canFire = false;
                StartCoroutine(Countdown());
            }
        }
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(turretCooldown);
        canFire = true;
    }
}
