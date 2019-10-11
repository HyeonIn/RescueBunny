using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int score = 0;
    public GUISkin GUISkin1;
    public static void setScore(int value)
    {
        score += value;
    }
    public static void initScore()
    {
        score = 0;
    }
    public static int getScore()
    {
        return score;
    }

    void OnGUI()
    {
        if (!GM.isEnded && !GM.isRestart)
        {        
            GUI.skin = GUISkin1;
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Score : "+ score.ToString());

            GUILayout.Space(15);
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

}
