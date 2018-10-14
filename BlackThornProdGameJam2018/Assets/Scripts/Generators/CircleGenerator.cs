using UnityEngine;

public class CircleGenerator : ShapeAbstractGenerator {
      
      private void Start() {
            
      }

      public override void GenerateShape(){
            this.transform.position =new Vector3(shapeSettings.posX, shapeSettings.posY, 0);
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