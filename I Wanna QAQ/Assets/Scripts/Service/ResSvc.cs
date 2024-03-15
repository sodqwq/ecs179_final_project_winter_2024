using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance;
    public XmlDocument dataDocument;
    public XmlNodeList dataNodeList;

    public void InitSvc()
    {
        Instance = this;
        InitSaveData();
    }
    private void InitSaveData()
    {
        string path = Application.dataPath + "/Resources/Data/saveData.xml";
        StreamReader xmlFile = new StreamReader(path);
        dataDocument = new XmlDocument();
        dataDocument.LoadXml(xmlFile.ReadToEnd());
        dataNodeList = dataDocument.SelectSingleNode("data").ChildNodes;
    }

    public struct SaveData
    {
        public string state;
        public int death;
        public string time;
        public string savePosition;
    }

    public SaveData GetSaveData(int dataChooseNum)
    {
        XmlElement element = (XmlElement)dataNodeList[dataChooseNum];
        SaveData saveData = new()
        {
            state = element.GetAttribute("state"),
            death = int.Parse(element.GetAttribute("death")),
            time = element.GetAttribute("time"),
            savePosition = element.GetAttribute("save_position")
        };
        return saveData;
    }
}
