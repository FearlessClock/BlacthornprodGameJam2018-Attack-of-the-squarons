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
    public SpellJson spellSettings;
    public Transform spellDirection;

    public float maxHp;
    public float currentHp;
    public float maxMana;
    public float currentMana;
    public float regenMana;

    public float spellCooldownTime;
    public float spellTimer;
    
    public bool spellReady;

    public void UpdateMana()
    {
        if(currentMana + regenMana <= maxMana)
        {
            currentMana += regenMana * Time.deltaTime;
        }
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

    public void LaunchSpell()
    {
        if(spellGenerator != null)
        {
            if(currentMana >= spellSettings.manaCost && spellReady)
            {
                GameObject spellObj = Instantiate<GameObject>(spellGenerator);

                spellObj.transform.parent = Spells;

                SpellGenerator spellScript = spellObj.GetComponent<SpellGenerator>();
                spellScript.spellSettings = spellSettings;
                spellScript.GenerateSpell(spellDirection);

                spellScript.ownerTag = this.gameObject.tag;
                currentMana -= spellSettings.manaCost;
                spellReady = false;
                spellTimer = spellCooldownTime;
            }
        }
    }
}
