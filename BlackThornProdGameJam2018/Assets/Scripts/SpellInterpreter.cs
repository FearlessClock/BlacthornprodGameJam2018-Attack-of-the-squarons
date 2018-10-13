
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class SpellInterpreter : MonoBehaviour {

    string[] functions;

    public GameObject spell;
    public GameObject circleShape;
    public GameObject lineShape;
    Dictionary<string, ShapeType> shapeDict;
	// Use this for initialization
	void Start () {        
        functions = new string[] { "circle", "line" };

        //Dict to convert from string to shapeType
        shapeDict = new Dictionary<string, ShapeType>();
        shapeDict.Add("circle", ShapeType.Circle);
        shapeDict.Add("line", ShapeType.Line);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InterpretScript(string code)
    {
        GameObject spellInstance = Instantiate(spell);
        Spell spellScript = spellInstance.GetComponent<Spell>();
        
        string[] sepCode = null;
        sepCode = code.Split(';');
        Phase currentPhase = new Phase();
        for (int j = 0; j < sepCode.Length - 1; j++)  //-1 because the line after the ; is counted with
        {
            string tillParan = sepCode[j].Split('(')[0];
            string paramsValues = sepCode[j].Split('(')[1].Replace(")", "");
            string[] paramsFunc = paramsValues.Split(',');
            for (int i = 0; i < functions.Length; i++)
            {
                if (tillParan.Length == functions[i].Length)
                {   
                    if (tillParan.Equals(functions[i]))
                    {
                        try
                        {
                            ShapeType shapeType;
                            shapeDict.TryGetValue(tillParan, out shapeType);

                            currentPhase.AddShape(CreateShape(shapeType, paramsFunc));
                        }
                        catch (ArgumentException)
                        {
                            Debug.Log("Doesn't exist");
                            throw;
                        }
                    }
                }
            }
        }
    }

    Shape CreateShape(ShapeType shapeType, string[] funcParams)
        {
            Shape shape;
            switch (shapeType)
            {
                case ShapeType.Circle:
                    //Check if there are enough params
                    shape = AddCircle(Convert.ToInt32(funcParams[0]), Convert.ToInt32(funcParams[1]), Convert.ToInt32(funcParams[2]));
                    break;
                case ShapeType.Line:
                    shape = AddLine(Convert.ToInt32(funcParams[0]), Convert.ToInt32(funcParams[1]), Convert.ToInt32(funcParams[2]), Convert.ToInt32(funcParams[3]));
                    break;
                default:
                    shape = null;
                    break;
            }

            return shape;
        }

        Shape AddCircle(int x, int y, int rad)
        {
            return Instantiate<GameObject>(circleShape).GetComponent<Shape>();
        }

        Shape AddLine(int x1, int y1, int x2, int y2)
        {
            return Instantiate<GameObject>(lineShape).GetComponent<Shape>();
        }
}
