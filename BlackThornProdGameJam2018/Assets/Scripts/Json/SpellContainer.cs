using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System; 
using TMPro;
public class HandleTextFile
{
    public static void WriteString(string path, string text)
    {
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
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


public class SpellContainer: MonoBehaviour {

    public string spellBookLocation;

    SpellJson spellSettings;

    public TextMeshProUGUI code;
    public SpellInterpreter spellInterpreter;

    public Creature creature;

    public Transform spellParent;

    SpellGenerator currentSpell;
    SpellJson currentSpellJson;

    private void Start() {
        string spellsText = HandleTextFile.ReadString(spellBookLocation);
        spellSettings = JsonUtility.FromJson<SpellJson>(spellsText);

        if (creature != null){
            creature.spellSettings = spellSettings;
        }
    }

    public void ReadCode(){
        foreach(Transform t in spellParent)
        {
            Destroy(t.gameObject, 0.1f);
        }
        //Interpret the code written by the player
        currentSpellJson = spellInterpreter.InterpretScript(code.text); 
        Debug.Log(JsonUtility.ToJson(currentSpellJson, true));
        //Turn the json into an actaul spell
        GameObject spellObj = Instantiate<GameObject>(creature.spellGenerator);
        spellObj.transform.parent = spellParent;
        currentSpell = spellObj.GetComponent<SpellGenerator>();
        currentSpell.spellSettings = currentSpellJson;
        currentSpell.GenerateSpell(spellObj.transform);
        //HandleTextFile.WriteString(spellBookLocation, JsonUtility.ToJson(spell, true));
    }

    public void saveCode()
    {
        HandleTextFile.WriteString(spellBookLocation, JsonUtility.ToJson(currentSpellJson, true));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
