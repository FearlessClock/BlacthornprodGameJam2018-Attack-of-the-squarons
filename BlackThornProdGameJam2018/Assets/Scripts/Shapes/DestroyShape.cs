using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShape : MonoBehaviour {

    public void Destroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
