using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitZone : MonoBehaviour {

    public Transform monsterHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.tag.Equals("Player") && monsterHolder.childCount == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
