using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkBubble : MonoBehaviour {

    public string[] words;
    public int currentPosition;
    public TextMeshProUGUI text;
    public GameObject UI;
    public string playerPrefString;
    // Use this for initialization
    void Start()
    {
        if(PlayerPrefs.GetInt(playerPrefString, 0) == 1)
        {
            UI.SetActive(false);
        }
        text.text = words[currentPosition];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextBubble()
    {
        currentPosition++;
        if(currentPosition >= words.Length)
        {
            UI.SetActive(false);
            PlayerPrefs.SetInt(playerPrefString, 1);
        }
        else
        {
            text.text = words[currentPosition];
        }
    }
}
