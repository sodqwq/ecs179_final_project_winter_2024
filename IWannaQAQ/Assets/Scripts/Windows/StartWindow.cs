using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindow : WindowRoot
{
    public RecordWindow recordWindow;

    protected override void InitWindow()
    {
        base.InitWindow();
    }

    // Start is called before the first frame update
    void Update()
    {
        EnterRecordWindow();
    }

    private void EnterRecordWindow()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Return))
        {
            SetWindowState(false);
            recordWindow.SetWindowState(true);
        }
    }
}
