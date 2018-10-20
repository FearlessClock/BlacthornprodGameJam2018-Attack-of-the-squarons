using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : SpellEffect {

    // Damage when staying in shape
    public float damInShape;
    public float damInShapeProcTime;
    private float damInShapeCurTime;

    // Slow 
    public float slow;
    public float slowOverTimeDuration;
    private float slowOverTimeCurDuration;

    public void InitEffect(float damInShape, float damInShapeProcTime, float slow, float slowOverTimeDuration)
    {
        this.damInShape = damInShape;
        this.damInShapeProcTime = damInShapeProcTime;

        this.slow = slow;
        this.slowOverTimeDuration = slowOverTimeDuration;

        damInShapeCurTime = damInShapeProcTime;
        slowOverTimeCurDuration = slowOverTimeDuration;
    }

    public override void ApplyEffect()
    {
        // Damage in shape time
        damInShapeCurTime -= Time.deltaTime;
        if (damInShapeCurTime <= 0)
        {
            monster.GetComponent<Creature>().Damage(damInShape);
            damInShapeCurTime = damInShapeProcTime;
        }

        // Slow
        slowOverTimeCurDuration -= Time.deltaTime;
        if (slowOverTimeCurDuration <= 0)
        {
            effectFinished = true;
        }
    }

    public override void UpdateEffect()
    {
        slowOverTimeCurDuration = slowOverTimeDuration;
    }

    public override void LeaveShapeArea()
    {
        damInShape = 0;
    }
}
