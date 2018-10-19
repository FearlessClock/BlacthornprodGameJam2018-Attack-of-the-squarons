using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuController : MonoBehaviour {

    public Rigidbody2D rigBody;
    public float movementSpeed;

    private void Awake()
    {
        rigBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        rigBody.MovePosition(transform.position + move * movementSpeed * Time.deltaTime);
    }
}
