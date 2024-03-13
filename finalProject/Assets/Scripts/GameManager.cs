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
        /*Vector3 vec = Vector3.zero;
        vec.x = PlayerPrefs.GetFloat("CameraPosX", 0);
        vec.y = PlayerPrefs.GetFloat("CameraPosY", 0);
        vec.z = PlayerPrefs.GetFloat("CameraPosZ", 0);
        Camera.main.transform.position = vec;
        vec.x = PlayerPrefs.GetFloat("playerPositionX", 0);
        vec.y = PlayerPrefs.GetFloat("playerPositionY", 0);
        vec.z = PlayerPrefs.GetFloat("playerPositionZ", 0);
        player.transform.position = vec;*/
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
