using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour {

    public GameObject canvas;
    public Transform monsterHolder;
    private bool gameFinished;
    // Use this for initialization
	void Start () {
        gameFinished = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(monsterHolder.childCount <= 0)
        {
            canvas.SetActive(true);
            gameFinished = true;
        }
	}
}
