using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; 
public class HandleTextFile
{
    public static void WriteString(string path, string text)
    {
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();
    }

    public static string ReadString(string path)
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        string text = reader.ReadToEnd();
        reader.Close();
        return text;
    }

}

[Serializable]
public class Spell{

      [NonSerialized]
	public List<Phase> phases = new List<Phase>();
      public Phase[] phasesArray;
	public void AddPhase(Phase p)
	{
		phases.Add(p);
	}

      public void finishPhaseAdding(){
            phasesArray = phases.ToArray();
      }

	public override string ToString()
	{
		string res = "";
		foreach(Phase p in phases)
		{
			res += p.ToString();

		}
		return res;
	}
}
public class SpellContainer: MonoBehaviour {

      public string spellBookLocation;

      Spell spell;

      private void Start() {
            string spellsText = HandleTextFile.ReadString(spellBookLocation);
            //spell = JsonUtility.FromJson<Spell>(spellsText);
            spell = new Spell();
            spell.AddPhase(new Phase());
            spell.phases[0].AddShape(new Shape());
            spell.phases[0].shapes[0].posX = 43;
            spell.phases[0].shapes[0].posY = 43;
            spell.phases[0].shapes[0].size = new Vector2(3, 2);
            spell.phases[0].shapes[0].type = "Circle".ToLower();
            spell.phases[0].shapes[0].duration = 3;
            spell.phases[0].shapes[0].elementalType = "Fire".ToLower();
            spell.phases[0].AddShape(new Shape());
            spell.phases[0].shapes[1].posX = 43;
            spell.phases[0].shapes[1].posY = 43;
            spell.phases[0].shapes[1].size = new Vector2(3, 2);
            spell.phases[0].shapes[1].type = "Circle".ToLower();
            spell.phases[0].shapes[1].duration = 3;
            spell.phases[0].shapes[1].elementalType = "Fire".ToLower();
            spell.phases[0].FinishShapeAdding();
            spell.AddPhase(new Phase());
            spell.phases[1].AddShape(new Shape());
            spell.phases[1].shapes[0].posX = 43;
            spell.phases[1].shapes[0].posY = 43;
            spell.phases[1].shapes[0].size = new Vector2(3, 2);
            spell.phases[1].shapes[0].type = "Circle".ToLower();
            spell.phases[1].shapes[0].duration = 3;
            spell.phases[1].shapes[0].elementalType = "Fire".ToLower();
            spell.phases[1].FinishShapeAdding();
            spell.finishPhaseAdding();

            string newjson = JsonUtility.ToJson(spell);
            Debug.Log(newjson);
            HandleTextFile.WriteString(spellBookLocation, newjson);
      }

	void Update(){

	}
}
