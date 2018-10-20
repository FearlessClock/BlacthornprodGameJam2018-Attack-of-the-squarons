using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Top level class for all characters, monsters and creature
/// </summary>
public class Creature: MonoBehaviour
{
    public float movementSpeed;
    public Transform Spells;
    public GameObject spellGenerator;
    public SpellJson spell1Settings;
    public SpellJson spell2Settings;
    public Transform spellDirection;

    public List<SpellEffect> spellEffects;

    // Update periodic effect damage
    public float timeBTWEffectUpdates;
    public float MaxTimeBTWEffectUpdates = 0.2f;

    public float maxHp;
    public float currentHp;
    public float maxMana;
    public float currentMana;
    public float regenMana;

    public float spellCooldownTime;
    public float spellTimer;
    
    public bool spellReady;

    public GameObject healthBar;
    public float initScaleXHB;

    public GameObject manaBar;

    public AudioSource audioSource;
    public AudioClip takenDamage;

    public float initScaleXMB; public void ApplyEffects()
    {
        if (spellEffects.Count > 0)
        {
            audioSource.PlayOneShot(takenDamage);
        }
        // Apply effects
        int i = 0;
        while (i < spellEffects.Count)
        {
            SpellEffect effect = spellEffects[i];
            effect.ApplyEffect();
            if (effect.effectFinished == true)
            {
                spellEffects.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    public void CreatureSetup()
    {
        currentHp = maxHp;
        healthBar = transform.GetChild(1).GetChild(0).gameObject;
        initScaleXHB = healthBar.transform.localScale.x;

        currentMana = maxMana;
        manaBar = transform.GetChild(1).GetChild(1).gameObject;
        initScaleXMB = manaBar.transform.localScale.x;

        spellEffects = new List<SpellEffect>();
    }

    public void UpdateMana()
    {
        currentMana += regenMana * Time.deltaTime;
        if(currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        Vector3 temp = manaBar.transform.localScale;
        temp.x = (currentMana / maxMana) * initScaleXMB;

        manaBar.transform.localScale = temp;
    }

    public void UpdateSpellCooldown()
    {
        if (!spellReady)
        {
            spellTimer -= Time.deltaTime;
            if(spellTimer <= 0)
            {
                spellReady = true;
            }
        }
    }

    public void LaunchSpell(int nmbr)
    {
        SpellJson currentSpell;
        if(spellGenerator != null)
        {
            switch (nmbr)
            {
                case 0:
                    currentSpell = spell1Settings;
                    break;
                case 1:
                    currentSpell = spell2Settings;
                    break;
                default:
                    currentSpell = null;
                    break;
            }
            if(currentMana >= currentSpell.manaCost && spellReady)
            {
                GameObject spellObj = Instantiate<GameObject>(spellGenerator);

                spellObj.transform.parent = Spells;

                SpellGenerator spellScript = spellObj.GetComponent<SpellGenerator>();
                spellScript.spellSettings = currentSpell;
                spellScript.GenerateSpell(spellDirection);

                spellScript.ownerTag = this.gameObject.tag;
                currentMana -= currentSpell.manaCost; Vector3 temp = manaBar.transform.localScale;
                temp.x = (currentMana / maxMana) * initScaleXMB;

                manaBar.transform.localScale = temp;
                spellReady = false;
                spellTimer = spellCooldownTime;
            }
        }
    }

    public void AddEffect(ElementalType type, int shapeId)
    {
        switch (type)
        {
            case ElementalType.fire:
                FireEffect fireEffect = new FireEffect();

                fireEffect.spellType = ElementalType.fire;
                fireEffect.shapeId = shapeId;
                fireEffect.monster = gameObject;

                fireEffect.InitEffect(10f, 1f, 5f, 1f, 10f);

                spellEffects.Add(fireEffect);
                break;

            case ElementalType.ice:
                IceEffect iceEffect = new IceEffect();

                iceEffect.spellType = ElementalType.ice;
                iceEffect.shapeId = shapeId;
                iceEffect.monster = gameObject;

                iceEffect.InitEffect(10f, 1f, 0.5f, 5f);

                spellEffects.Add(iceEffect);
                break;

            case ElementalType.earth:
                earthEffect earthEffect = new earthEffect();

                earthEffect.spellType = ElementalType.earth;
                earthEffect.shapeId = shapeId;
                earthEffect.monster = gameObject;

                earthEffect.InitEffect(10f, 1f, 5f, 1f, 10f);

                spellEffects.Add(earthEffect);
                break;

            case ElementalType.death:
                deathEffect deathEffect = new deathEffect();

                deathEffect.spellType = ElementalType.death;
                deathEffect.shapeId = shapeId;
                deathEffect.monster = gameObject;

                deathEffect.InitEffect(10f, 1f, 5f, 1f, 10f);

                spellEffects.Add(deathEffect);
                break;

            case ElementalType.poison:
                poisonEffect poisonEffect = new poisonEffect();

                poisonEffect.spellType = ElementalType.poison;
                poisonEffect.shapeId = shapeId;
                poisonEffect.monster = gameObject;

                poisonEffect.InitEffect(10f, 1f, 5f, 1f, 10f);

                spellEffects.Add(poisonEffect);
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
            if (spellEffects[i].shapeId == shapeId)
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
        if (sg != null && !sg.ownerTag.Equals(this.tag))
        {
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

    public void Damage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            if(gameObject.tag == "Monster")
            {
                Kill();
            }
            else if(gameObject.tag == "Player")
            {
                SceneManager.LoadScene(1);
            }
        }

        Vector3 temp = healthBar.transform.localScale;
        temp.x = (currentHp / maxHp) * initScaleXHB;

        healthBar.transform.localScale = temp;
    }

    public void Kill()
    {
        gameObject.GetComponent<Animator>().SetTrigger("kill");
    }
}
