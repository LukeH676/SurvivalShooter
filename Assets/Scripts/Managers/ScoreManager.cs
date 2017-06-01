using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> (); // references the text component
        score = 0;// sets score to 0 evreytime we start game
    }


    void Update ()
    {
        text.text = "Score: " + score; // derp change the content of text component
    }
}
