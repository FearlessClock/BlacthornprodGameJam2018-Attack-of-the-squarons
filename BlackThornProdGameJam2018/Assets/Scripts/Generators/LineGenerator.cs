using UnityEngine;

public class LineGenerator : ShapeAbstractGenerator {
      private void Start() {
      }

      public override void GenerateShape(Vector3 spellLocation){
            
            this.transform.position =new Vector3(shapeSettings.posX, shapeSettings.posY, 0) + spellLocation;
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
      }

      
}