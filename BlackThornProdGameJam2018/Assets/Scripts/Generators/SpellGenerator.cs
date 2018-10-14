using UnityEngine;

public class SpellGenerator : MonoBehaviour {
      
      public SpellJson spellSettings;

      public GameObject phase;

      private void Start() {
      }

      public void GenerateSpell(Vector3 spellLocation){
            for(int i = 0; i < spellSettings.phasesArray.Length; i++){
                  GameObject phaseObj = Instantiate<GameObject>(phase);
                  phaseObj.transform.parent = this.transform;
                  PhaseGenerator phaseScript = phaseObj.GetComponent<PhaseGenerator>();
                  phaseScript.phaseSettings = spellSettings.phasesArray[i];
                  phaseScript.GenerateShapes(spellLocation);
            }      
      }
}