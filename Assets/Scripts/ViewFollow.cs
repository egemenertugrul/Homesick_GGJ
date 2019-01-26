using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFollow : MonoBehaviour
{
    public Vector3 set;

    public Transform car;
	// Use this for initialization
	void Start ()
	{
	    set = transform.position - car.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position = set + car.position;
	}
}
