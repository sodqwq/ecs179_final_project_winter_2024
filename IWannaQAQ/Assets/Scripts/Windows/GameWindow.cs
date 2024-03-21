using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameWindow : WindowRoot
{
    public GameObject player;
    public GameObject gameOverTip;
    public Transform startPoint;
    public GameObject[] levelArr;

    public GameObject heroKnight;
    private int levelCount;
    public SavePoint initialSavePoint;
    public StartWindow startWindow;

    protected override void InitWindow()
    {
        base.InitWindow();
        levelCount = 0;
        GameStart();
        LoadLevel();
    }

    private void GameStart()
    {
        player.SetActive(true);
        gameOverTip.SetActive(false);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LastSavePoint", -1);
    }
    private void LoadLevel()
    {
        Debug.Log(levelCount);
        if(levelCount > 3){
            levelCount = 0;
        }
        GameObject level = Instantiate(levelArr[levelCount]);
        level.name = "Level" + levelCount;
        level.transform.SetParent(transform, false);

        //player.transform.localPosition = level.transform.Find("StartPoint").localPosition;
        int savepoint = PlayerPrefs.GetInt("LastSavePoint");
        if (savepoint != -1)
        {
            player.transform.localPosition = level.transform.Find("Save_" + savepoint).localPosition;

            /*SavePoint[] allSavePoints = FindObjectsOfType<SavePoint>(); 
            GameObject player = GameObject.FindWithTag("Player"); 
            foreach (var savePoint in allSavePoints)
            {
                if (savePoint.id == levelCount) // check if the save point is in the current level
                {
                    //SavePoint.LoadPlayerPosition(player);  
                    float x = PlayerPrefs.GetFloat("playerPositionX");
                    float y = PlayerPrefs.GetFloat("playerPositionY");
                    float z = PlayerPrefs.GetFloat("playerPositionZ");
                    player.transform.position = new Vector3(x, y, z);
                    //player.transform.
                    Debug.Log("LoadPlayerPosition");
                    break; 
                }
            }*/
        }
        else
        {
            player.transform.localPosition = level.transform.Find("StartPoint").localPosition;
            // Debug.Log("StartPoint");
        }
    }

    private void DeleteLevel()
    {
        Destroy(transform.Find("Level" + levelCount).gameObject);
    }

    public void NextLevel()
    {
        DeleteLevel();
        levelCount++;
        PlayerPrefs.SetInt("LastSavePoint", 1); 
        LoadLevel();
        
    }

    public void GameOver()
    {
        player.SetActive(false);
        gameOverTip.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void Restart()
    {
        GameStart();
        DeleteLevel();
        LoadLevel();
    }

    private void ExitGame()
    {
        SetWindowState(false);
        startWindow.SetWindowState(true);
    }
}
