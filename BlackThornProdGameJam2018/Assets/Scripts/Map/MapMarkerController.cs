using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMarkerController : MonoBehaviour {
    private Animator animator;
    public float fadeTime;
    public float fadeSpeed;
    public int levelID;
    private Camera cameraObj;
	// Use this for initialization
	void Start () {
        cameraObj = Camera.main;
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
    }

    public void LaunchFadeOutAsCoRoutine()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        Vector3 target = this.transform.position;
        target.z = cameraObj.transform.position.z;
        while (Camera.main.orthographicSize > 0)
        {
            cameraObj.orthographicSize -= fadeSpeed * Time.deltaTime;
            cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, target, 0.2f);
            if (Camera.main.orthographicSize < 0.05f)
            {
                Camera.main.orthographicSize = 0;
            }
            yield return new WaitForSeconds(fadeTime);
        }
        SceneManager.LoadScene(levelID);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter");
        if (collision.tag.Equals("Player"))
        {
            animator.SetBool("Touched", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            animator.SetBool("Touched", false);
        }
    }
}
