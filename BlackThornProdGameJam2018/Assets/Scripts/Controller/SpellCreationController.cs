using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellCreationController : MonoBehaviour {
    public int levelId;
    public Image fadeOutPanel;
    public FadeValues fadeValues;

    public void LoadMainMenu()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            Debug.Log(alpha);
            fadeOutPanel.color = new Color(0, 0, 0, alpha += Time.deltaTime * fadeValues.panelFadeOutSpeed);
            yield return new WaitForSeconds(fadeValues.fadeTime);
        }
        SceneManager.LoadScene(levelId);
    }
}
