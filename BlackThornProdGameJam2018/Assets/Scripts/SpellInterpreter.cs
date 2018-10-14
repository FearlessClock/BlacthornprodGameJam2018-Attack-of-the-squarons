
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class SpellInterpreter : MonoBehaviour {

    string[] functions;

    public string delayName = "delay";
    Dictionary<string, ShapeType> shapeDict;
	// Use this for initialization
	void Start () {
        functions = new string[] { "delay", "circle", "triangle", "square" };

        //Dict to convert from string to shapeType
        shapeDict = new Dictionary<string, ShapeType>();
        shapeDict.Add("circle", ShapeType.Circle);
        shapeDict.Add("triangle", ShapeType.Triangle);
        shapeDict.Add("square", ShapeType.Square);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public SpellJson InterpretScript(string code)
    {
        SpellJson spellJson = new SpellJson();
        
        string[] sepCode = null;
        sepCode = code.Split(';');
        PhaseJson currentPhase = new PhaseJson();
        for (int j = 0; j < sepCode.Length - 1; j++)  //-1 because the line after the ; is counted with
        {
            string tillParan = sepCode[j].Split('(')[0].ToLower();
            string paramsValues = sepCode[j].Split('(')[1].Replace(")", "");
            string[] paramsFunc = paramsValues.Split(',');
            for (int i = 0; i < functions.Length; i++)
            {
                if (tillParan.Length == functions[i].Length)
                {
                    if(tillParan.Equals(delayName)){
                        Debug.Log("End a phase");
                        currentPhase.phaseDuration = (float)Convert.ToDouble(paramsFunc[0]);
                        currentPhase.FinishShapeAdding();
                        spellJson.AddPhase(currentPhase);
                        currentPhase = new PhaseJson();
                    }else if (tillParan.Equals(functions[i]))
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
        if(currentPhase.shapes.Count > 0){
            currentPhase.phaseDuration = (float)Convert.ToDouble(0);
            currentPhase.FinishShapeAdding();
            spellJson.AddPhase(currentPhase);
        }
        spellJson.finishPhaseAdding(); 
        return spellJson;
    }

    ShapeJson CreateShape(ShapeType shapeType, string[] funcParams)
        {
            ShapeJson shape;
            switch (shapeType)
            {
                case ShapeType.Circle:
                    //Check if there are enough params
                    shape = AddCircle(Convert.ToInt32(funcParams[0]), Convert.ToInt32(funcParams[1]), Convert.ToInt32(funcParams[2]));
                    break;
                case ShapeType.Triangle:
                    shape = AddSquare(Convert.ToInt32(funcParams[0]), Convert.ToInt32(funcParams[1]), Convert.ToInt32(funcParams[2]), Convert.ToInt32(funcParams[3]));
                    break;
                case ShapeType.Square:
                    shape = AddTriangle(Convert.ToInt32(funcParams[0]), Convert.ToInt32(funcParams[1]), Convert.ToInt32(funcParams[2]), Convert.ToInt32(funcParams[3]));
                    break;
                default:
                    shape = null;   
                    break;
            }

            return shape;
        }

    ShapeJson AddCircle(int x, int y, int rad)
    {
        ShapeJson shapeJson = new ShapeJson();
        shapeJson.posX = x;
        shapeJson.posY = y;
        shapeJson.damage = 1;
        shapeJson.size = Vector2.one * rad;
        shapeJson.duration = 3;
        shapeJson.elementalType = "fire";
        shapeJson.manaCost = 2;
        shapeJson.type = "circle";
        return shapeJson;
    }

    ShapeJson AddTriangle(int x1, int y1, int x2, int y2)
    {
        ShapeJson shapeJson = new ShapeJson();
        shapeJson.posX = x1;
        shapeJson.posY = y1;
        shapeJson.damage = 1;
        shapeJson.size = Vector2.one * x2;
        shapeJson.duration = 3;
        shapeJson.elementalType = "ice";
        shapeJson.manaCost = y2;
        shapeJson.type = "triangle";
        return shapeJson;
    }

    ShapeJson AddSquare(int x1, int y1, int x2, int y2)
    {
        ShapeJson shapeJson = new ShapeJson();
        shapeJson.posX = x1;
        shapeJson.posY = y1;
        shapeJson.damage = 1;
        shapeJson.size = Vector2.one * x2;
        shapeJson.duration = 3;
        shapeJson.elementalType = "water";
        shapeJson.manaCost = y2;
        shapeJson.type = "square";
        return shapeJson;
    }
}
