using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseGenerator : MonoBehaviour {
    public PhaseJson phaseSettings;

    public GameObject circlePrefab;
    public GameObject squarePrefab;
    public GameObject trianglePrefab;

    public float time;

    public Transform spellLocation;


    private void Start() {
            
      }

    public void GenerateShapes(Transform spellLoc){
        spellLocation = spellLoc;
        this.transform.position = spellLoc.position;
        this.transform.rotation = Quaternion.Inverse(spellLoc.rotation);
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
                    shape = Instantiate<GameObject>(squarePrefab, new Vector3(phaseSettings.shapesArray[i].posX, phaseSettings.shapesArray[i].posY), Quaternion.identity);
                    break;
                case "triangle":
                    shape = Instantiate<GameObject>(trianglePrefab, new Vector3(phaseSettings.shapesArray[i].posX, phaseSettings.shapesArray[i].posY), Quaternion.identity);
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
                shapeScript.GenerateShape(spellLocation.position);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                print("Shape script is nuil");
            }
        }
    }
}