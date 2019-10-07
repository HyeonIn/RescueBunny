using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    static int score = 0;

    public static void setScore(int value)
    {
        score += value;
    }

    public static int getScore()
    {
        return score;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0,0, Screen.width, Screen.height));
        GUILayout.Label("Money :"+ score.ToString());
    }

}
