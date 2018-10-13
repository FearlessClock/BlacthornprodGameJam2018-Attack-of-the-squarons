using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

      void Start(){
            Destroy(this.gameObject, 17);
      }

	public override string ToString()
	{
		return "Circle (" + 1 + " : " + 1 + ") " ;
	}
}
