using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

    public float maxHp;
    private float currentHp;

    private  List<SpellEffect> spellEffects;

    private GameObject healthBar;
    private float initScaleX;

    // Update periodic effect damage
    private float timeBTWEffectUpdates;
    private float MaxTimeBTWEffectUpdates = 0.2f;

    // Use this for initialization
    void Start () {
        timeBTWEffectUpdates = MaxTimeBTWEffectUpdates;
        spellEffects = new List<SpellEffect>();
        healthBar = transform.GetChild(1).GetChild(0).gameObject;
        currentHp = maxHp;
        initScaleX = healthBar.transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {

        // Apply effects
        int i = 0;
        while(i < spellEffects.Count)
        {
            SpellEffect effect = spellEffects[i];
            effect.ApplyEffect();
            if(effect.effectFinished == true)
            {
                spellEffects.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        timeBTWEffectUpdates -= Time.deltaTime; // Time to update over time effects
    }

    public void Damage(float damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            currentHp = 0;
            // DESTROY MONSTER //////////////////////
        }

        Vector3 temp = healthBar.transform.localScale;
        temp.x = (currentHp / maxHp) * initScaleX;

        healthBar.transform.localScale = temp;
    }

    public void AddEffect(ElementalType type, int shapeId)
    {
        switch (type)
        {
            case ElementalType.normal: // CHANGE TO NORMAL EFFECT
                FireEffect norEffect = new FireEffect();

                norEffect.spellType = ElementalType.fire;
                norEffect.shapeId = shapeId;
                norEffect.monster = gameObject;

                norEffect.InitEffect(10, 1, 5, 1, 10);

                spellEffects.Add(norEffect);
                break;
            case ElementalType.fire:
                FireEffect fireEffect = new FireEffect();

                fireEffect.spellType = ElementalType.fire;
                fireEffect.shapeId = shapeId;
                fireEffect.monster = gameObject;

                fireEffect.InitEffect(10, 1, 5, 1, 10);

                spellEffects.Add(fireEffect);
                break;
            case ElementalType.water:
                break;
            case ElementalType.ice:
                break;
            case ElementalType.ground:
                break;
            case ElementalType.death:
                break;
            case ElementalType.poison:
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
        switch(collision.tag)
        {
            case "SpellShape":
                AddEffect(collision.gameObject.GetComponent<CircleGenerator>().elementalType, collision.gameObject.GetInstanceID());
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(timeBTWEffectUpdates <= 0)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "SpellShape":
                EffectLeaveShapeArea(collision.gameObject.GetInstanceID());
                break;
        }
    }
}
