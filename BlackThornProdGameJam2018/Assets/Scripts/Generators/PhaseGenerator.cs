using UnityEngine;

public class PhaseGenerator : MonoBehaviour {
    public PhaseJson phaseSettings;

    public GameObject circlePrefab;
    public GameObject squarePrefab;
    public GameObject trianglePrefab;

    private void Start() {
            
      }

    public void GenerateShapes(Vector3 spellLocation){
        for(int i = 0; i < phaseSettings.shapesArray.Length; i++){
            GameObject shape = null;
            ShapeAbstractGenerator shapeScript = null;
            switch (phaseSettings.shapesArray[i].type)
            {
                case "circle":
                    shape = Instantiate<GameObject>(circlePrefab);
                    break;
                case "square":
                    shape = Instantiate<GameObject>(squarePrefab);
                    break;
                case "triangle":
                    shape = Instantiate<GameObject>(trianglePrefab);
                    break;
            }
            shape.transform.parent = this.transform;
            shapeScript = shape.GetComponent<ShapeAbstractGenerator>();
            shapeScript.shapeSettings = phaseSettings.shapesArray[i];
            shapeScript.GenerateShape(spellLocation);
        }
    }
}