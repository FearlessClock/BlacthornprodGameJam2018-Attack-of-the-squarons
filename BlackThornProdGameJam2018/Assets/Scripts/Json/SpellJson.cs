using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpellJson{

    [NonSerialized]
	public List<PhaseJson> phases = new List<PhaseJson>();
      
    public PhaseJson[] phasesArray;
    public float manaCost;
    public string code;
	public void AddPhase(PhaseJson p)
	{
		phases.Add(p);
	}

    public void finishPhaseAdding(){
        phasesArray = phases.ToArray();
    }

	public override string ToString()
	{
		string res = "";
		foreach(PhaseJson p in phases)
		{
			res += p.ToString();

		}
		return res;
	}
}