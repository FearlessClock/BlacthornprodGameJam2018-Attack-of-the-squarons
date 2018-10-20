using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("GameScene", 3);
	}

    public void GameScene()
    {
        SceneManager.LoadScene(1);
    }
}
