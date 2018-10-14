using UnityEngine;

public abstract class ShapeAbstractGenerator : MonoBehaviour {
    public ShapeJson shapeSettings;

    public Animator animator;

    public ElementalType elementalType;

    public abstract void GenerateShape(Vector3 spellLocation);
}