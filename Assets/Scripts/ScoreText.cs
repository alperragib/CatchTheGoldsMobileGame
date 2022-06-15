using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public static int scoreValue = 0;
    public static int old_scoreValue = 0;
    private Color[] colors = new Color[7];
    Text score;
    int i=0;
    void Start()
    {
        score = GetComponent<Text>();

        Color cyan = new Color();
        Color yellow = new Color();
        Color dark = new Color();
        Color blue = new Color();
        Color green = new Color();
        Color red = new Color();
        ColorUtility.TryParseHtmlString("#01ffff", out cyan);
        ColorUtility.TryParseHtmlString("#FFAE3B", out yellow);
        ColorUtility.TryParseHtmlString("#292929", out dark);
        ColorUtility.TryParseHtmlString("#00838a", out blue);
        ColorUtility.TryParseHtmlString("#3EB489", out green);
        ColorUtility.TryParseHtmlString("#f85e56", out red);
        colors[0]= yellow;
        colors[1] = green;
        colors[2] = red;
        colors[3] = blue;
        colors[4] = dark;
        colors[5] = cyan;
        colors[6] = cyan;
    }
    
    void Update()
    {
        

        score.text = scoreValue.ToString();

        if (old_scoreValue!=scoreValue && scoreValue-old_scoreValue>=50) {
            old_scoreValue = scoreValue;
            Camera.main.backgroundColor = colors[i];
            i++;
            if (i>=colors.Length) {
                i = 0;
                old_scoreValue = 0;
            }
        }
    }
}
