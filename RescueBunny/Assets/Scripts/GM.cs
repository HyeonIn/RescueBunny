using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public GameObject player;
    public GUISkin modernGUISkin;
    Vector3 StartingPos;
    Vector3 EndingPos;
    bool isStarted = false;
    public static bool isEnded = false;
    public static bool isRestart = false;


    void Awake() {
        Time.timeScale = 0f;    
    }
    // Start is called before the first frame update
    void Start()
    {
        StartingPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        
        EndingPos = GameObject.FindGameObjectWithTag("End").transform.position;
        player = GameObject.FindGameObjectWithTag("Character");
    }
    void OnGUI() {
        GUI.skin = modernGUISkin;
        if (!isStarted)
        {
        GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        
        GUILayout.Label("Press Start Button");

        if (GUILayout.Button("Start"))
        {
            isStarted = true;
            StartGame();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        
        GUILayout.EndArea();    
        }

        // 끝날 때
        else if (isEnded)
        {
        GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        
        GUILayout.Label("You rescue the pretty Bunny! :D");
        int totalScore = ScoreManager.getScore() + (MovingCharacter.getHealth() * 2000);
        GUILayout.Label("Your Score : "+totalScore.ToString());

        if (GUILayout.Button("Restart?"))
        {
            ScoreManager.initScore();
            SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
            isEnded = false;
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        
        GUILayout.EndArea();    
        }
        
        // 죽었을 때
        else if (isRestart)
        {
        GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        
        GUILayout.Label("Oh! You die... But you Can Restart!");

        if (GUILayout.Button("Restart?"))
        {
            ScoreManager.initScore();
            SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
            isRestart = false;
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        
        GUILayout.EndArea();    
        }
        

    }

    void StartGame()
    {
        Time.timeScale = 1f;

        GameObject standingCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
    }

    public static void EndGame()
    {
        Time.timeScale = 0f;
        
        isEnded = true;
    }

    public static void RestartStage()
    {
        Time.timeScale = 0f;
        isRestart = true;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
