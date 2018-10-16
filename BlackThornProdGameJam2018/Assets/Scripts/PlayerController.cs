using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed;
    public Transform Spells;
    public GameObject spellGenerator;

    public SpellJson spellSettings;

    public Rigidbody2D rigBody;

    private void Awake()
    {
        rigBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        rigBody.MovePosition(transform.position + move * speed * Time.deltaTime);

        //this.transform.position += move * speed * Time.deltaTime;

        if(Input.GetButtonUp("Fire1"))
        {
            GameObject spellObj = Instantiate<GameObject>(spellGenerator);

            spellObj.transform.parent = Spells;

            SpellGenerator spellScript = spellObj.GetComponent<SpellGenerator>();
            spellScript.spellSettings = spellSettings;
            spellScript.GenerateSpell(transform.position);
        }
	}
}
