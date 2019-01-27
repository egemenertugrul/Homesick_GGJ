using SimpleKeplerOrbits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public bool assignRandomTimeScales = false;
    public float scaleMultiplier = 3f;

    public enum e_MapType { SolarSystem, Asteroid }
    public e_MapType mapType;

    public float universalTimeScale = 1;

    KeplerOrbitMover[] keplerMovers;

    public GameObject[] earthSimplePlanetPrefabs;
    public GameObject[] asteroidPrefabs;

    public string nextSceneName = "a_map_3";

    // Use this for initialization
    void Start()
    {
        keplerMovers = GetComponentsInChildren<KeplerOrbitMover>();
        foreach (KeplerOrbitMover mover in keplerMovers)
        {
            MeshRenderer mr = mover.gameObject.GetComponent<MeshRenderer>();
            if (mr) Destroy(mr);
            MeshFilter mf = mover.gameObject.GetComponent<MeshFilter>();
            if (mf) Destroy(mf);

            if (mapType == e_MapType.SolarSystem && earthSimplePlanetPrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, earthSimplePlanetPrefabs.Length);
                GameObject newPlanet = Instantiate(earthSimplePlanetPrefabs[randomIndex], mover.transform);
                float sphereScale = mover.transform.localScale.x; // Assuming x,y,z scales are the same
                newPlanet.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
            } else if(mapType == e_MapType.Asteroid && asteroidPrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, asteroidPrefabs.Length);
                GameObject newAsteroid = Instantiate(asteroidPrefabs[randomIndex], mover.transform);
                //float sphereScale = mover.transform.localScale.x; // Assuming x,y,z scales are the same
                newAsteroid.transform.localScale = new Vector3(newAsteroid.transform.localScale.x * scaleMultiplier, newAsteroid.transform.localScale.y * scaleMultiplier, newAsteroid.transform.localScale.z * scaleMultiplier);
                int randomRotX = Random.Range(0, 360);
                int randomRotY = Random.Range(0, 360);
                int randomRotZ = Random.Range(0, 360);
                newAsteroid.transform.eulerAngles = new Vector3(randomRotX, randomRotY, randomRotZ);
                if (assignRandomTimeScales)
                {
                    float randomTimeScale = Random.Range(1.0f, 500.0f);
                    mover.TimeScale = randomTimeScale;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!assignRandomTimeScales)
        {
            foreach (KeplerOrbitMover mover in keplerMovers)
            {
                mover.TimeScale = universalTimeScale;
            }
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
