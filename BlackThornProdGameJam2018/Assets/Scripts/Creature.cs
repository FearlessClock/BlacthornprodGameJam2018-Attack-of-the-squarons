using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
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
    public float initScaleXMB;

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
}
