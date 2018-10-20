using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthEffect : SpellEffect
{

    // Damage when staying in shape
    public float damInShape;
    public float damInShapeProcTime;
    private float damInShapeCurTime;

    // Damage over time 
    public float damOverTime;
    public float damOverTimeProcTime;
    private float damOverTimeCurTime;
    public float damOverTimeDuration;
    private float damOverTimeCurDuration;

    public void InitEffect(float damInShape, float damInShapeProcTime, float damOverTime, float damOverTimeProcTime, float damOverTimeDuration)
    {
        this.damInShape = damInShape;
        this.damInShapeProcTime = damInShapeProcTime;

        this.damOverTime = damOverTime;
        this.damOverTimeProcTime = damOverTimeProcTime;
        this.damOverTimeDuration = damOverTimeDuration;

        damInShapeCurTime = damInShapeProcTime;
        damOverTimeCurTime = damOverTimeProcTime;
        damOverTimeCurDuration = damOverTimeDuration;
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

        // Damage over time
        damOverTimeCurTime -= Time.deltaTime;
        damOverTimeCurDuration -= Time.deltaTime;
        if (damOverTimeCurDuration <= 0)
        {
            effectFinished = true;
        }

        if (damOverTimeCurTime <= 0)
        {
            monster.GetComponent<MonsterController>().Damage(damOverTime);
            damOverTimeCurTime = damOverTimeProcTime;
        }
    }

    public override void UpdateEffect()
    {
        damOverTimeCurDuration = damOverTimeDuration;
    }

    public override void LeaveShapeArea()
    {
        damInShape = 0;
    }
}
