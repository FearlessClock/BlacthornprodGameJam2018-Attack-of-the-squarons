using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : MonoBehaviour {

    public GameObject monster;
    public ElementalType spellType;
    public int shapeId;

    public bool effectFinished = false;

    public abstract void ApplyEffect();
    public abstract void UpdateEffect();
    public abstract void LeaveShapeArea();
}
