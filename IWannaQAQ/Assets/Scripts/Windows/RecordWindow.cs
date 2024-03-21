using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;   // Text

public class RecordWindow : WindowRoot
{
    public PlayerControl playerController;
    public GameObject[] dataArr;
    public GameWindow gameWindow;
    public StartWindow startWindow;
    [HideInInspector]
    public int dataChooseNum;   // Don't want to show in inspector

    protected override void InitWindow()
    {
        base.InitWindow();
        ShowData();
    }

    private void ShowData()
    {
        for(int i=0; i<dataArr.Length; i++)
        {
            ResSvc.SaveData saveData = resourceSvc.GetSaveData(i);
            string state = saveData.state;
            string death = "Death:" + saveData.death.ToString();
            string time = "Time:" + saveData.time;
            dataArr[i].transform.GetChild(1).GetComponent<Text>().text = state;
            dataArr[i].transform.GetChild(2).GetComponent<Text>().text = death;
            dataArr[i].transform.GetChild(3).GetComponent<Text>().text = time;
        }
    }

    private void Update()
    {
        ChangeChoose();
        EnterGame();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
        
    }

    private void ChangeChoose()
    {
        RectTransform dataChoose = transform.Find("DataChoose").GetComponent<RectTransform>();
        float posX = dataChoose.localPosition.x;
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            posX = posX >= 300.0f ? -300.0f : posX + 300f;
            dataChooseNum = dataChooseNum >= 2 ? 0 : dataChooseNum + 1;
            dataChoose.localPosition = new Vector2(posX, dataChoose.localPosition.y);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            posX = posX <= -300.0f ? 300.0f : posX - 300f;
            dataChooseNum = dataChooseNum <= 0 ? 2 : dataChooseNum - 1;
            dataChoose.localPosition = new Vector2(posX, dataChoose.localPosition.y);
        }
    }

    private void EnterGame()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Return))
        {
            playerController.InitPlayer();
            SetWindowState(false);
            gameWindow.SetWindowState(true);
        }
    }
    private void ExitGame()
    {
        SetWindowState(false);
        startWindow.SetWindowState(true);
    }
}
