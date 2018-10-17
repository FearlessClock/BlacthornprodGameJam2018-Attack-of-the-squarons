using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMovement : MonoBehaviour {

	void Start () {
		gameObject.GetComponent<Animator>().Play(0, -1, Random.Range(0f, 1f));
    }
}
