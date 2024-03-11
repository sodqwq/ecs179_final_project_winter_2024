using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        float x = PlayerPrefs.GetFloat("playerPositionX", 0);
        float y = PlayerPrefs.GetFloat("playerPositionY", 0);
        float z = PlayerPrefs.GetFloat("playerPositionZ", 0);
        player.transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Restarting Game");
            SceneManager.LoadScene("MainScene");
        }
    }
}
