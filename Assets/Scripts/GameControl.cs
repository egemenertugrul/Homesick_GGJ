using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    

    public GameObject image;

    public GameObject button;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SkipVideo()
    {
        image.SetActive(false);
        Destroy(button);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}

