using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    static float distanceMultiplier = 10f;

    public CarScript car;
    private Vector3 directionOfCarFromPlanet;

    private float earthG = 9.8f;
    private float forceEarth;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        directionOfCarFromPlanet = Vector3.zero;
    }

    //void Update()
    //{
    //    directionOfCarFromPlanet = (transform.position - car.position).normalized;
    //    car.GetComponent<Rigidbody>().AddForce(directionOfCarFromPlanet * gravitationalForce);
    //    print(directionOfCarFromPlanet * gravitationalForce);
    //}

    void FixedUpdate()
    {
        // Do the Force calculation (refer universal gravitation for more info)
        // Use numbers to adjust force, distance will be changing over time!
        forceEarth = earthG * ((rb.mass * car.rb.mass) / Mathf.Pow(Vector3.Distance(transform.position, car.transform.position) * distanceMultiplier, 2));
        // Find the Normal direction
        Vector3 normalDirection = (transform.position - car.transform.position).normalized;

        // calculate the force on the object from the planet
        Vector3 normalForce = normalDirection * forceEarth;

        // Calculate for the other systems on your solar system similarly
        // Apply all these forces on current planet's rigidbody

        // Apply the force on the rigid body of the surrounding object/s
        car.GetComponent<Rigidbody>().AddForce(normalForce);
        // .... add forces of other objects. 
    }

    private void OnTriggerEnter(Collider other)
    {
        CarScript cs = other.gameObject.GetComponent<CarScript>();
        if (cs)
        {
            if (gameObject.tag != "Earth") // TODO: VERY DANGEROUS CODE; CHANGE IT
            {
                StartCoroutine(CountdownToEndGame());
            }
        }
    }

    private IEnumerator CountdownToEndGame()
    {
        yield return new WaitForSeconds(1f);

        GetComponentInParent<MapManager>().LoadNextLevel();

        // write here
    }
}
