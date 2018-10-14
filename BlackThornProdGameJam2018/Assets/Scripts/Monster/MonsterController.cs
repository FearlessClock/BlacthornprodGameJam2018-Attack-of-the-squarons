using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

    public float maxHp;
    private float currentHp;

    private Rigidbody2D rigBody;

    private  List<SpellEffect> spellEffects;

    private GameObject healthBar;
    private float initScaleX;

    // Update periodic effect damage
    private float timeBTWEffectUpdates;
    private float MaxTimeBTWEffectUpdates = 0.2f;

    // Use this for initialization
    void Start () {
        rigBody = gameObject.GetComponent<Rigidbody2D>();

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
            Kill();
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
        switch(collision.tag)
        {
            case "CircleSpell":
                AddEffect(collision.gameObject.GetComponent<CircleGenerator>().elementalType, collision.gameObject.GetInstanceID());
                break;
            case "SquareSpell":
                AddEffect(collision.gameObject.GetComponent<SquareGenerator>().elementalType, collision.gameObject.GetInstanceID());
                break;
            case "TriangleSpell":
                AddEffect(collision.gameObject.GetComponent<TriangleGenerator>().elementalType, collision.gameObject.GetInstanceID());
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

    public void Kill()
    {
        gameObject.GetComponent<Animator>().SetTrigger("kill");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
