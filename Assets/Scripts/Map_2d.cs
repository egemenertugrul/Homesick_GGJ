using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_2d : MonoBehaviour
{
    public GameObject[] map_2d;//map_2d[0] is car (all in the canvas with image)
    public GameObject[] map_3d;//map_3d[0] is car (add the gameObject you want to show in the map)

    public float ratio3dTo2d = 2;
		
	//RectPosY -180~450
    //RectPosX -470~460

	void Update () {
             
	    for (int i = 1; i < map_2d.Length; i++)
	    {
	        map_2d[i].GetComponent<RectTransform>().anchoredPosition = Convert3DPosInto2DPos(map_3d[i]);
        }
	    
    }




    public Vector2 Convert3DPosInto2DPos(GameObject target)
    {
        float xRate;
        float yRate;
        float target_2d_x;
        float target_2d_y;
        xRate = map_3d[0].transform.position.z - target.transform.position.z;
        target_2d_x = map_2d[0].GetComponent<RectTransform>().anchoredPosition.x + xRate / ratio3dTo2d;
        yRate= map_3d[0].transform.position.x - target.transform.position.x;
        target_2d_y = map_2d[0].GetComponent<RectTransform>().anchoredPosition.y - yRate / ratio3dTo2d;
        return new Vector2(target_2d_x, target_2d_y);
    }
}
