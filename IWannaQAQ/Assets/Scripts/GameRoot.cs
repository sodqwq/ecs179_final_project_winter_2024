using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameRoot Instance;
    public StartWindow startWindow;

    void Start()
    {
        Instance = this;
        ClearWindow();  // Always Start Window First
        InitGame();
    }

    private void ClearWindow()  // Hide all the windows
    {
        Transform canvas = transform.Find("Canvas");
        for(int i=0; i<canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        startWindow.SetWindowState(true);
    }

    private void InitGame()
    {
        ResSvc resSvc = GetComponent<ResSvc>();
        resSvc.InitSvc();
    }
}
