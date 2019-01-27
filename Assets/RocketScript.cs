using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

    public static float lifetime = 60f; // seconds
    public static float defaultRocketSpeed = 14000f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AsteroidScript>() != null)
        {
            Destroy(gameObject);
            Destroy(other.GetComponent<MeshRenderer>());
            other.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject, 5f);
        }
    }
}
