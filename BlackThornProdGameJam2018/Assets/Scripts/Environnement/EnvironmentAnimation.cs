using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnimation : MonoBehaviour {

    Animator anim;

	void Start () {
        anim = gameObject.GetComponent<Animator>();
        anim.Play(0, -1, Random.Range(0f, 1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Monster")
        {
            anim.SetTrigger("Touch");
        }
    }
}
