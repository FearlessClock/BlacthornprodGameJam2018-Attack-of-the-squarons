using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Creature {
    

    public Rigidbody2D rigBody;

    private void Awake()
    {
        rigBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        rigBody.MovePosition(transform.position + move * movementSpeed * Time.deltaTime);

        //this.transform.position += move * speed * Time.deltaTime;

        if(Input.GetButtonUp("Fire1"))
        {
            LaunchSpell();
        }
	}
}
