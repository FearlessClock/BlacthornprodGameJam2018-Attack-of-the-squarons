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
            case "ice":
                elementalType = ElementalType.ice;
                break;
            case "earth":
                elementalType = ElementalType.earth;
                break;
            case "death":
                elementalType = ElementalType.death;
                break;
            case "poison":
                elementalType = ElementalType.poison;
                break;
            default:
                elementalType = ElementalType.fire;
                break;
        }
        Invoke("destroyWithAnimator", shapeSettings.duration);
    }

    void destroyWithAnimator()
    {
        animator.SetTrigger("destroy");
    }
}
