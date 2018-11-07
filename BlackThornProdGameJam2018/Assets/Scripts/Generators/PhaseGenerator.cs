using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseGenerator : MonoBehaviour {
    public PhaseJson phaseSettings;

    public GameObject[] circlePrefab;
    public GameObject[] squarePrefab;
    public GameObject[] trianglePrefab;

    public float time;

    public Transform spellLocation;


    private void Start() {
            
      }

    public void GenerateShapes(Transform spellLoc){
        spellLocation = spellLoc;
        this.transform.SetPositionAndRotation(spellLoc.position, Quaternion.Inverse(spellLoc.rotation));
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
                    switch(phaseSettings.shapesArray[i].elementalType)
                    {
                        case "fire":
                            shape = Instantiate<GameObject>(circlePrefab[0]);
                            break;
                        case "ice":
                            shape = Instantiate<GameObject>(circlePrefab[1]);
                            break;
                        case "earth":
                            shape = Instantiate<GameObject>(circlePrefab[2]);
                            break;
                        case "death":
                            shape = Instantiate<GameObject>(circlePrefab[3]);
                            break;
                        case "poison":
                            shape = Instantiate<GameObject>(circlePrefab[4]);
                            break;
                    }
                    break;
                case "square":
                    switch (phaseSettings.shapesArray[i].elementalType)
                    {
                        case "fire":
                            shape = Instantiate<GameObject>(squarePrefab[0]);
                            break;
                        case "ice":
                            shape = Instantiate<GameObject>(squarePrefab[1]);
                            break;
                        case "earth":
                            shape = Instantiate<GameObject>(squarePrefab[2]);
                            break;
                        case "death":
                            shape = Instantiate<GameObject>(squarePrefab[3]);
                            break;
                        case "poison":
                            shape = Instantiate<GameObject>(squarePrefab[4]);
                            break;
                    }
                    break;
                case "triangle":
                    switch (phaseSettings.shapesArray[i].elementalType)
                    {
                        case "fire":
                            shape = Instantiate<GameObject>(trianglePrefab[0]);
                            break;
                        case "ice":
                            shape = Instantiate<GameObject>(trianglePrefab[1]);
                            break;
                        case "earth":
                            shape = Instantiate<GameObject>(trianglePrefab[2]);
                            break;
                        case "death":
                            shape = Instantiate<GameObject>(trianglePrefab[3]);
                            break;
                        case "poison":
                            shape = Instantiate<GameObject>(trianglePrefab[4]);
                            break;
                    }
                    break;
                default:
                    Debug.Log(phaseSettings.shapesArray[i].type);
                    shape = Instantiate<GameObject>(squarePrefab[0]);
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