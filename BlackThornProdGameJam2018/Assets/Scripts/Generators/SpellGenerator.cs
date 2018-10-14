using System.Collections;
using UnityEngine;

public class SpellGenerator : MonoBehaviour
{

    public SpellJson spellSettings;

    public GameObject phase;

    public Vector3 spellLocation;

    private float timeBTWChecks = 0.2f;

    private void Start()
    {
    }

    private void Update()
    {
        if(timeBTWChecks <= 0)
        {
            if (transform.childCount == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                foreach (Transform child in transform)
                {
                    if (child.childCount == 0)
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
            timeBTWChecks = 0.2f;
        }
        else
        {
            timeBTWChecks -= Time.deltaTime;
        }
    }

    public void GenerateSpell(Vector3 spellLoc)
    {
        spellLocation = spellLoc;
        StartCoroutine(CoroutineGenerateShape());
    }

    IEnumerator CoroutineGenerateShape()
    {
        for (int i = 0; i < spellSettings.phasesArray.Length; i++)
        {
            GameObject phaseObj = Instantiate<GameObject>(phase);
            phaseObj.transform.parent = this.transform;
            PhaseGenerator phaseScript = phaseObj.GetComponent<PhaseGenerator>();
            phaseScript.phaseSettings = spellSettings.phasesArray[i];
            phaseScript.GenerateShapes(spellLocation);
            yield return new WaitForSeconds(spellSettings.phasesArray[i].phaseDuration);
        }
    }
}