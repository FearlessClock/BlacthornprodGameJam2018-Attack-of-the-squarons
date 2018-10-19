using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Monster: Creature
{
    public List<SpellEffect> spellEffects;
    //Enemy AI stats
    public float sightRange;
    public float attackRange;
    public float scaredRange;

    private GameObject healthBar;
    private float initScaleX;

    public float distanceToPlayer;

    public void MonsterSetup()
    {
        healthBar = transform.GetChild(1).GetChild(0).gameObject;
        initScaleX = healthBar.transform.localScale.x;
        
        spellEffects = new List<SpellEffect>();
        currentHp = maxHp;
        currentMana = maxMana;
    }

    public void ApplyEffects()
    {
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

    public void Kill()
    {
        gameObject.GetComponent<Animator>().SetTrigger("kill");
    }
    public void Damage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            Kill();
        }

        Vector3 temp = healthBar.transform.localScale;
        temp.x = (currentHp / maxHp) * initScaleX;

        healthBar.transform.localScale = temp;
    }
}
