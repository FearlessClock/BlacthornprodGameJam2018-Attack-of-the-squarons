using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonsterController : Monster {


    //Enemy AI stats
    public float collisionCheckSize;
    public LayerMask wallLayerMask;
    public LayerMask playerLayerMask;
    
    public Rigidbody2D rigidbodyComp;

    // Use this for initialization
    void Start ()
    {
        CreatureSetup();
        rigidbodyComp = this.GetComponent<Rigidbody2D>();
        timeBTWEffectUpdates = MaxTimeBTWEffectUpdates;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMana();
        UpdateSpellCooldown();
        ApplyEffects();
        timeBTWEffectUpdates -= Time.deltaTime; // Time to update over time effects
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, distanceToPlayer);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, scaredRange);
    }
}
