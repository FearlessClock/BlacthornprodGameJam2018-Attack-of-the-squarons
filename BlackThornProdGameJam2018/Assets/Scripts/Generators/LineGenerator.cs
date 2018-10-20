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
       }
}