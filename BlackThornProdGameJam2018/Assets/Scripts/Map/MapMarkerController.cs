using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMarkerController : MonoBehaviour {
    private Animator animator;
    public int levelID;
    private Camera cameraObj;
    public Image fadeOutPanel;
    public FadeValues fadeValues;
    private bool startTransition;

	void Start () {
        cameraObj = Camera.main;
        animator = GetComponent<Animator>();
	}

    public void LaunchFadeOutAsCoRoutine()
    {
        if (!startTransition)
        {
            StartCoroutine("FadeOut");
            startTransition = true;
        }
    }

    IEnumerator FadeOut()
    {
        Vector3 target = this.transform.position;
        target.z = cameraObj.transform.position.z;
        float alpha = 0;
        while (Camera.main.orthographicSize > 0.5)
        {
            fadeOutPanel.color = new Color(0, 0, 0, alpha+= Time.deltaTime * fadeValues.panelFadeOutSpeed);
            cameraObj.orthographicSize -= fadeValues.fadeSpeed * Time.deltaTime;
            cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, target, 0.2f);
            if (Camera.main.orthographicSize < 0.1f)
            {
                Camera.main.orthographicSize = 0;
            }
            yield return new WaitForSeconds(fadeValues.fadeTime);
        }
        SceneManager.LoadScene(levelID);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
