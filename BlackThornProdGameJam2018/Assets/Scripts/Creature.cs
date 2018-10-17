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

    public int direction;

    public float maxHp;
    public float currentHp;
    public float maxMana;
    public float currentMana;
    public float regenMana;

    public void UpdateMana()
    {
        if(currentMana + regenMana < maxMana)
        {
            currentMana += regenMana;
        }
    }

    public void LaunchSpell()
    {
        if(spellGenerator != null)
        {
            if(currentMana > spellSettings.manaCost)
            {
                GameObject spellObj = Instantiate<GameObject>(spellGenerator);

                spellObj.transform.parent = Spells;

                SpellGenerator spellScript = spellObj.GetComponent<SpellGenerator>();
                spellScript.spellSettings = spellSettings;
                spellScript.GenerateSpell(spellDirection);

                currentMana -= spellSettings.manaCost;
            }
        }
    }
}
