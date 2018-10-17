using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Creature {
    

    public Rigidbody2D rigBody;
    private float threshold = 0;
    public float rotationSpeed;

    private void Awake()
    {
        rigBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Quaternion rotation = new Quaternion();
        if (Input.GetAxis("Horizontal") > threshold)
        {
            rotation = Quaternion.Euler(0, 0, -90);
        }else if(Input.GetAxis("Horizontal") < threshold)
        {
            rotation = Quaternion.Euler(0, 0, 90);
        }
        else if(Input.GetAxis("Vertical") > threshold)
        {
            rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(Input.GetAxis("Vertical") < threshold)
        {
            rotation = Quaternion.Euler(0, 0, 180);
        }

        spellDirection.transform.rotation = Quaternion.Lerp(spellDirection.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        //This commented code works so well but doesn't stay in place when I let go
        ///this.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg, Vector3.forward);
        rigBody.MovePosition(transform.position + move * movementSpeed * Time.deltaTime);

        //this.transform.position += move * speed * Time.deltaTime;

        if(Input.GetButtonUp("Fire1"))
        {
            LaunchSpell();
        }
	}
}
