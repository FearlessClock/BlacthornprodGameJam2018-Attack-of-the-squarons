using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Creature {

	void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        this.transform.position += move * movementSpeed * Time.deltaTime;

        if(Input.GetButtonUp("Fire1"))
        {
            LaunchSpell();
        }
	}
}
