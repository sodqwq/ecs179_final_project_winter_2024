using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : WindowRoot
{
    public GameObject player;
    public GameObject gameOverTip;
    public Transform startPoint;
    public GameObject[] levelArr;
    private int levelCount;
    public SavePoint initialSavePoint;

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

    private void LoadLevel()
    {
        Debug.Log(levelCount);
        GameObject level = Instantiate(levelArr[levelCount]);
        level.name = "Level" + levelCount;
        Debug.Log(level.name);
        level.transform.SetParent(transform, false);

        //player.transform.localPosition = level.transform.Find("StartPoint").localPosition;

        int lastSavePointId = PlayerPrefs.GetInt("LastSavePoint", -1); // get the last save point
        if (lastSavePointId != -1)
        {
            SavePoint[] allSavePoints = FindObjectsOfType<SavePoint>(); 
            GameObject player = GameObject.FindWithTag("Player"); 
            foreach (var savePoint in allSavePoints)
            {
                if (savePoint.id == levelCount) // check if the save point is in the current level
                {
                    //SavePoint.LoadPlayerPosition(player); // 
                    float x = PlayerPrefs.GetFloat("playerPositionX");
                    float y = PlayerPrefs.GetFloat("playerPositionY");
                    float z = PlayerPrefs.GetFloat("playerPositionZ");
                    player.transform.localPosition = new Vector3(x, y, z);
                    break; 
                }
            }
        }
        else
        {
            player.transform.localPosition = level.transform.Find("StartPoint").localPosition;
            Debug.Log("No save point yet");
        }
    }

    private void DeleteLevel()
    {
        Debug.Log("Deleteeeeeeee?");
        Destroy(transform.Find("Level" + levelCount).gameObject);
    }

    public void NextLevel()
    {
        Debug.Log("???????????????????????");
        DeleteLevel();
        levelCount++;
        LoadLevel();
    }

    public void GameOver()
    {
        player.SetActive(false);
        gameOverTip.SetActive(true);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        GameStart();
        DeleteLevel();
        LoadLevel();
    }
}
