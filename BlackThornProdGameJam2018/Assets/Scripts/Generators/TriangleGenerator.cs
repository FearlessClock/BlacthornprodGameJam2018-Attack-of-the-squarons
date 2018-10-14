using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGenerator : ShapeAbstractGenerator
{
    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    public override void GenerateShape(Vector3 spellLocation)
    {
        this.transform.position = new Vector3(shapeSettings.posX, shapeSettings.posY, 0) + spellLocation;
        this.transform.localScale = Vector3.one * shapeSettings.size;
        switch (shapeSettings.elementalType)
        {
            case "fire":
                elementalType = ElementalType.fire;
                break;
            case "water":
                elementalType = ElementalType.water;
                break;
            default:
                elementalType = ElementalType.normal;
                break;
        }
        Invoke("destroyWithAnimator", shapeSettings.duration);
    }

    void destroyWithAnimator()
    {
        animator.SetTrigger("destroy");
    }
}
