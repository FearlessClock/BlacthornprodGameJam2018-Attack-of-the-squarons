using UnityEngine;

public class PhaseGenerator : MonoBehaviour {
      public PhaseJson phaseSettings;

      public GameObject circlePrefab;
      public GameObject linePrefab;

      private void Start() {
            
      }

      public void GenerateShapes(Vector3 spellLocation){
            for(int i = 0; i < phaseSettings.shapesArray.Length; i++){
                  GameObject shape;
                  ShapeAbstractGenerator shapeScript = null;
                  switch (phaseSettings.shapesArray[i].type)
                  {
                      case "circle":
                        shape = Instantiate<GameObject>(circlePrefab);
                        shape.transform.parent = this.transform;
                        shapeScript = shape.GetComponent<ShapeAbstractGenerator>();
                        break;
                      case "line":
                        shape = Instantiate<GameObject>(linePrefab);
                        shape.transform.parent = this.transform;
                        shapeScript = shape.GetComponent<ShapeAbstractGenerator>();
                        break;                  
                  }
                  if(shapeScript != null){
                        shapeScript.shapeSettings = phaseSettings.shapesArray[i];
                        shapeScript.GenerateShape(spellLocation);
                  }
            }
      }
}