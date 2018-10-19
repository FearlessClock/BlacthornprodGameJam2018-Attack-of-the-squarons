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


    // Update periodic effect damage
    private float timeBTWEffectUpdates;
    private float MaxTimeBTWEffectUpdates = 0.2f;

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

    public void AddEffect(ElementalType type, int shapeId)
    {
        switch (type)
        {
            case ElementalType.normal: // CHANGE TO NORMAL EFFECT
                NormalEffect norEffect = new NormalEffect();

                norEffect.spellType = ElementalType.normal;
                norEffect.shapeId = shapeId;
                norEffect.monster = gameObject;

                norEffect.InitEffect(10f, 1f);

                spellEffects.Add(norEffect);
                break;
            case ElementalType.fire:
                FireEffect fireEffect = new FireEffect();

                fireEffect.spellType = ElementalType.fire;
                fireEffect.shapeId = shapeId;
                fireEffect.monster = gameObject;

                fireEffect.InitEffect(10f, 1f, 5f, 1f, 10f);

                spellEffects.Add(fireEffect);
                break;
            case ElementalType.water:
                break;
            case ElementalType.ice:
                IceEffect iceEffect = new IceEffect();

                iceEffect.spellType = ElementalType.normal;
                iceEffect.shapeId = shapeId;
                iceEffect.monster = gameObject;

                iceEffect.InitEffect(10f, 1f, 0.5f, 5f);

                spellEffects.Add(iceEffect);
                break;
            case ElementalType.earth:
                break;
            case ElementalType.death:
                break;
            case ElementalType.poison:
                break;
            case ElementalType.lightning:
                break;
            default:
                break;
        }
    }

    public void RemoveEffectFromList(int shapeId)
    {
        bool foundShape = false;
        int i = 0;
        while (!foundShape && i < spellEffects.Count)
        {
            if(spellEffects[i].shapeId == shapeId)
            {
                foundShape = true;
                spellEffects.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    public void EffectLeaveShapeArea(int shapeId)
    {
        bool foundShape = false;
        int i = 0;
        while (!foundShape && i < spellEffects.Count)
        {
            if (spellEffects[i].shapeId == shapeId)
            {
                foundShape = true;
                spellEffects[i].LeaveShapeArea();
            }
            else
            {
                i++;
            }
        }
    }

    public void EffectUpdateWithinShapeArea(int shapeId)
    {
        bool foundShape = false;
        int i = 0;
        while (!foundShape && i < spellEffects.Count)
        {
            if (spellEffects[i].shapeId == shapeId)
            {
                foundShape = true;
                spellEffects[i].UpdateEffect();
            }
            else
            {
                i++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpellGenerator sg = collision.gameObject.transform.parent.parent.GetComponent<SpellGenerator>();
        if (sg != null && !sg.ownerTag.Equals(this.tag)){
            //By using the inheritence of the shape classes, we can get the super class and get information from it.
            AddEffect(collision.gameObject.GetComponent<ShapeAbstractGenerator>().elementalType, collision.gameObject.GetInstanceID());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        SpellGenerator sg = collision.gameObject.transform.parent.parent.GetComponent<SpellGenerator>();

        if (sg != null && !sg.ownerTag.Equals(this.tag))
        {
            if (timeBTWEffectUpdates <= 0)
            {
                switch (collision.tag)
                {
                    case "SpellShape":
                        EffectUpdateWithinShapeArea(collision.gameObject.GetInstanceID());
                        break;
                }
                timeBTWEffectUpdates = MaxTimeBTWEffectUpdates;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "SpellShape":
                EffectLeaveShapeArea(collision.gameObject.GetInstanceID());
                break;
        }
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
