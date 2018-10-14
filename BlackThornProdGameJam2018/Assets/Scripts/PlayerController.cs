using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

      public int speed; 
      
      public SpellGenerator spell;
      // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        this.transform.position += move * speed * Time.deltaTime;

        if(Input.GetButtonUp("Fire1")){
              spell.GenerateSpell(this.transform.position);
        }
	}
}
