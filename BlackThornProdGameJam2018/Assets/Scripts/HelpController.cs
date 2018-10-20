using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour {

    public GameObject areaSprite;

	void Start () {
		
	}

    public void SwitchAreaSprite()
    {
        areaSprite.SetActive(!areaSprite.activeSelf);
    }
}
