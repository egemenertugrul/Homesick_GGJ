using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestVideo : MonoBehaviour
{
    private Image mr;
    public MovieTexture movieTexture;
	// Use this for initialization
	void Start ()
	{
        mr=GetComponent<Image>();
	   // rImage = GetComponent<RawImage>();
	    mr.material.mainTexture = movieTexture;
	    movieTexture.Play();
	    //播放模式设置为循环播放
	    movieTexture.loop = false;
	    
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
