using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HQController : MonoBehaviour {

    Animator HQAnimator;
    public int levelId;
    public FadeValues fadeValues;
    public Image fadeOutPanel;

    private void Start()
    {
        HQAnimator = GetComponent<Animator>();
    }

    public void LoadLevel(int levelId)
    {
        SceneManager.LoadScene(levelId);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HQAnimator.SetTrigger("Effect");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetButtonUp("Fire1"))
        {
            SceneManager.LoadScene(levelId);
        }
    }

    IEnumerator FadeOut()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            fadeOutPanel.color = new Color(0, 0, 0, alpha += Time.deltaTime * fadeValues.panelFadeOutSpeed);
            yield return new WaitForSeconds(fadeValues.fadeTime);
        }
        SceneManager.LoadScene(levelId);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HQAnimator.SetTrigger("Effect");
        }
    }
}
