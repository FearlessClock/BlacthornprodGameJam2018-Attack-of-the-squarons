using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkBubble : MonoBehaviour {

    public string[] words;
    public int currentPosition;
    public TextMeshProUGUI text;
    public GameObject UI;
    // Use this for initialization
    void Start()
    {
        text.text = words[currentPosition];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextBubble()
    {
        Debug.Log("Wesh");
        currentPosition++;
        if(currentPosition >= words.Length)
        {
            UI.SetActive(false);
        }
        text.text = words[currentPosition];
    }
}
