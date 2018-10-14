using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseGenerator : MonoBehaviour {
    public PhaseJson phaseSettings;

    public GameObject circlePrefab;
    public GameObject squarePrefab;
    public GameObject trianglePrefab;

    public float time;

    public Vector3 spellLocation;


    private void Start() {
            
      }

    public void GenerateShapes(Vector3 spellLoc){
        spellLocation = spellLoc;
        StartCoroutine(CoroutineGenerateShape());
    }

    IEnumerator CoroutineGenerateShape()
    {
        for (int i = 0; i < phaseSettings.shapesArray.Length; i++)
        {
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
                default:
                    Debug.Log(phaseSettings.shapesArray[i].type);
                    shape = Instantiate<GameObject>(squarePrefab);
                    break;

            }
            shape.transform.parent = this.transform;
            shapeScript = shape.GetComponent<ShapeAbstractGenerator>();
            if(shapeScript != null)
            {
                shapeScript.shapeSettings = phaseSettings.shapesArray[i];
                shapeScript.GenerateShape(spellLocation);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                print("Shape script is nuil");
            }
        }
    }
}