using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		if(monsterHolder.childCount <= 0 && !gameFinished)
        {
            canvas.SetActive(true);
            gameFinished = true;
        }
	}

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene(7);
    }

    public void NextScene()
    {
        int sceneId = SceneManager.GetActiveScene().buildIndex;
        if(sceneId < 6 && sceneId > 1)
        {
            sceneId += 1;
            SceneManager.LoadScene(sceneId);
        }
    }
}
