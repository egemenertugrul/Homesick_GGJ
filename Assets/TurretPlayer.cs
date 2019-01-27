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
    public float turretCooldown = 1f;
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
            turretModel.transform.LookAt(new Vector3(cam.ScreenPointToRay(lastFire).direction.x * 800, cam.ScreenPointToRay(lastFire).direction.y * 600, cam.ScreenPointToRay(lastFire).direction.z * 600));
            Debug.DrawRay(cam.transform.position, cam.ScreenPointToRay(lastFire).direction);

            lastFired = true;
            if (canFire)
            {
                GameObject newRocket = Instantiate(rocket);
                newRocket.transform.position = turretModel.transform.position;
                newRocket.transform.eulerAngles = new Vector3(turretModel.transform.eulerAngles.x, turretModel.transform.eulerAngles.y - 90, turretModel.transform.eulerAngles.z);
                newRocket.GetComponent<Rigidbody>().AddForce(cam.ScreenPointToRay(lastFire).direction * RocketScript.defaultRocketSpeed, ForceMode.Acceleration);
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
