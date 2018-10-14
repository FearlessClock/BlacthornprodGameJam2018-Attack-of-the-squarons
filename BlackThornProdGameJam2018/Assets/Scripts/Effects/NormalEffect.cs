﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEffect : SpellEffect
{
    // Damage when staying in shape
    public float damInShape;
    public float damInShapeProcTime;
    private float damInShapeCurTime;
    
    public void InitEffect(float damInShape, float damInShapeProcTime)
    {
        this.damInShape = damInShape;
        this.damInShapeProcTime = damInShapeProcTime;

        damInShapeCurTime = damInShapeProcTime;
    }

    public override void ApplyEffect()
    {
        // Damage in shape time
        damInShapeCurTime -= Time.deltaTime;
        if (damInShapeCurTime <= 0)
        {
            monster.GetComponent<MonsterController>().Damage(damInShape);
            damInShapeCurTime = damInShapeProcTime;
        }
    }

    public override void UpdateEffect()
    {
    }

    public override void LeaveShapeArea()
    {
        damInShape = 0;
        effectFinished = true;
    }
}