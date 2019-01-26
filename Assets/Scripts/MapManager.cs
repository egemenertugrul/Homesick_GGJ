using SimpleKeplerOrbits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public float universalTimeScale = 1;

    KeplerOrbitMover[] keplerMovers;

    public GameObject[] earthSimplePlanetPrefabs;

	// Use this for initialization
	void Start () {
        keplerMovers = GetComponentsInChildren<KeplerOrbitMover>();
        foreach(KeplerOrbitMover mover in keplerMovers)
        {
            MeshRenderer mr = mover.gameObject.GetComponent<MeshRenderer>();
            if(mr) Destroy(mr);
            MeshFilter mf = mover.gameObject.GetComponent<MeshFilter>();
            if(mf) Destroy(mf);
            int randomIndex = Random.Range(0, earthSimplePlanetPrefabs.Length);

            GameObject newPlanet = Instantiate(earthSimplePlanetPrefabs[randomIndex], mover.transform);
            float sphereScale = mover.transform.localScale.x; // Assuming x,y,z scales are the same
            newPlanet.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach (KeplerOrbitMover mover in keplerMovers)
        {
            mover.TimeScale = universalTimeScale;
        }
    }
}
