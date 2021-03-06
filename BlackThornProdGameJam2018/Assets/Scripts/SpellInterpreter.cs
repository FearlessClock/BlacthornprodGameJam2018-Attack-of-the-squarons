﻿
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellInterpreter : MonoBehaviour {

    string[] functions;

    public float distanceWeight;
    public float maxDistance;
    public float sizeWeight;
    public float maxSize;
    public float damageWeight;
    public float maxDamage;
    public float durationWeight;
    public float maxDuration;

    public TextMeshProUGUI consoleText;

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
        spellJson.code = code;
        code = code.Replace("\n", "");
        sepCode = code.Split(';');
        PhaseJson currentPhase = new PhaseJson();
        for (int j = 0; j < sepCode.Length - 1; j++)  //-1 because the line after the ; is counted with
        {
            string tillParan = sepCode[j].Split('(')[0].ToLower();
            string paramsValues = sepCode[j].Split('(')[1].Replace(")", "");
            string[] paramsFunc = paramsValues.Split(',');
            bool foundFunction = false;
            for (int i = 0; i < functions.Length; i++)
            {
                if (tillParan.Length == functions[i].Length)
                {
                    if(tillParan.Equals(delayName)){
                        currentPhase.phaseDuration = (float)Convert.ToDouble(paramsFunc[0]);
                        currentPhase.FinishShapeAdding();
                        spellJson.AddPhase(currentPhase);
                        currentPhase = new PhaseJson();
                        foundFunction = true;
                    }
                    else if (tillParan.Equals(functions[i]))
                    {
                            try
                            {
                                ShapeType shapeType;
                                shapeDict.TryGetValue(tillParan, out shapeType);

                                currentPhase.AddShape(CreateShape(shapeType, paramsFunc));
                                foundFunction = true;
                            }
                            catch (ArgumentException)
                            {
                                LogToConsole(tillParan + " doesn't exist");
                                Debug.Log("Doesn't exist");
                                throw;
                            }
                    }
                }
            }
            if (!foundFunction)
            {
                LogToConsole("The " + tillParan + " function doesn't exist");
                Debug.Log("The "+ tillParan +" function doesn't exist");
            }
        }
        if(currentPhase.shapes.Count > 0){
            currentPhase.phaseDuration = (float)Convert.ToDouble(0);
            currentPhase.FinishShapeAdding();
            spellJson.AddPhase(currentPhase);
        }
        spellJson.finishPhaseAdding();
        foreach (PhaseJson phase in spellJson.phasesArray)
        {
            spellJson.manaCost += phase.GetManaCost();
        }
        return spellJson;
    }

    ShapeJson CreateShape(ShapeType shapeType, string[] funcParams)
    {
        ShapeJson shape;
        string type = "";
        switch (shapeType)
        {
            case ShapeType.Circle:
                //Check if there are enough params
                if(funcParams.Length == 6)
                {
                    type = "circle";
                }
                break;
            case ShapeType.Triangle:
                if (funcParams.Length == 6)
                {
                    type = "triangle";
                }
                break;
            case ShapeType.Square:
                if (funcParams.Length == 6)
                {
                    type = "square";
                }
                break;
            default:
                shape = null;   
                break;
        }
        if (funcParams.Length == 6)
        {
            try
            {
                shape = AddShape(Convert.ToDouble(funcParams[0]), Convert.ToDouble(funcParams[1]), Convert.ToDouble(funcParams[2]), Convert.ToDouble(funcParams[3]), Convert.ToDouble(funcParams[4]), funcParams[5], type);
            }
            catch (FormatException)
            {
                LogToConsole("There was a format error, check the parameters");
                throw;
            }
        }
        else if(funcParams.Length > 6)
        {
            shape = null;
            LogToConsole("There are only 6 parameters");
            Debug.Log("There are only 6 parameters");
        }
        else
        {
            shape = null;
            LogToConsole("There are 6 parameters");
            Debug.Log("There are 6 parameters");
        }
        return shape;
    }

    //x, y, damage, duration, size, damageType
    ShapeJson AddShape(double x, double y, double damage, double duration, double size, string damageType, string type)
    {
        ShapeJson shapeJson = new ShapeJson();
        Vector2 spellPos = new Vector2((float)x, (float)y);
        if(spellPos.magnitude > maxDistance)
        {
            spellPos = spellPos.normalized * maxDistance;
        }
        shapeJson.posX = spellPos.x;
        shapeJson.posY = spellPos.y;
        shapeJson.damage = damage < maxDamage? (float)damage : maxDamage;
        shapeJson.size = size < maxSize? (float)size : maxSize;
        shapeJson.duration = duration < maxDuration? (float)duration : maxDuration;
        shapeJson.elementalType = damageType;
        //get an equation that will give us the manacost in function of the damage, position, size and duration
        shapeJson.manaCost = distanceWeight * (spellPos.magnitude/maxDistance) + 
                                durationWeight * (shapeJson.duration /maxDuration) + 
                                    sizeWeight * (shapeJson.size /maxSize) + 
                                        damageWeight * (shapeJson.damage / maxDamage);
        shapeJson.type = type;
        return shapeJson;
    }

    public void LogToConsole(string log)
    {
        consoleText.text = "> " + log + "\n"+consoleText.text;
    }
}
