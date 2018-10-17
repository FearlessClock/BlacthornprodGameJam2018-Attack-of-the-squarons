using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
    public GameObject[] mapPoints;
    public int mapPosition;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.S))
        {
            if(mapPosition + 1 < mapPoints.Length)
            {
                mapPosition += 1;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (mapPosition - 1 >= 0)
            {
                mapPosition -= 1;
            }
        }
	}
}
