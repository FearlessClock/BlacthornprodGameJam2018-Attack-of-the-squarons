using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEffect : SpellEffect
{

    public void InitEffect(float damInShape, float damInShapeProcTime, float damOverTime, float damOverTimeProcTime, float damOverTimeDuration)
    {
        /*
        this.damInShape = damInShape;
        this.damInShapeProcTime = damInShapeProcTime;

        this.damOverTime = damOverTime;
        this.damOverTimeProcTime = damOverTimeProcTime;
        this.damOverTimeDuration = damOverTimeDuration;

        damInShapeCurTime = damInShapeProcTime;
        damOverTimeCurTime = damOverTimeProcTime;
        damOverTimeCurDuration = damOverTimeDuration;
        */
    }

    public override void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void LeaveShapeArea()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateEffect()
    {
        throw new System.NotImplementedException();
    }
}
