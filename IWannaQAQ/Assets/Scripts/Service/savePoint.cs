using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Sprite saveSuccess;
    // each save point has a unique ID
    public int id; 
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Beshot()
    {
        SavePlayerPosition();
        // save the last save point
        PlayerPrefs.SetInt("LastSavePoint", id); 

        spriteRenderer.sprite = saveSuccess;
    }

    void SavePlayerPosition()
    {   
        // save the data
        PlayerPrefs.SetFloat("playerPositionX", transform.position.x);
        PlayerPrefs.SetFloat("playerPositionY", transform.position.y);
        PlayerPrefs.SetFloat("playerPositionZ", transform.position.z);
        PlayerPrefs.Save(); 
    }

    public static void LoadPlayerPosition(GameObject player)
    {
        // load the data
        if (PlayerPrefs.HasKey("playerPositionX") && PlayerPrefs.HasKey("playerPositionY") && PlayerPrefs.HasKey("playerPositionZ"))
        {
            float x = PlayerPrefs.GetFloat("playerPositionX");
            float y = PlayerPrefs.GetFloat("playerPositionY");
            float z = PlayerPrefs.GetFloat("playerPositionZ");
            player.transform.position = new Vector3(x, y, z);
        }
        else
        {
            Debug.Log("No saved player position found.");
        }
    }
}


