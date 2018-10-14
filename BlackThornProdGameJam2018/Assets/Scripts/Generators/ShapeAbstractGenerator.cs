using UnityEngine;

public abstract class ShapeAbstractGenerator : MonoBehaviour {
      public ShapeJson shapeSettings;

      public int duration;

      public ElementalType elementalType;

      public abstract void GenerateShape();
}