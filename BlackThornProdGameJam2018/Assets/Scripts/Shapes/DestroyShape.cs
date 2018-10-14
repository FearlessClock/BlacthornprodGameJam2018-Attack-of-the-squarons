using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShape : MonoBehaviour {

    public void Destroy()
    {
        Invoke("DestroyObject", 2f);
        transform.GetChild(1).GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }

    public void DestroyObject()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
