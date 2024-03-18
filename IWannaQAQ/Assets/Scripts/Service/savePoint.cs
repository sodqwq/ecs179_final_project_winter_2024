using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Sprite saveSuccess;
    // each save point has a unique ID
    public int id; 
    private SpriteRenderer spriteRenderer;

    private float playerPositionX;
    private float playerPositionY;
    private float playerPositionZ;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Beshot()
    {   
        int lastSavePoint = PlayerPrefs.GetInt("LastSavePoint");
        PlayerPrefs.SetInt("LastSavePoint", id); 
        // save the data
        if(lastSavePoint != id)
        {
            spriteRenderer.sprite = saveSuccess;
        }
        //spriteRenderer.sprite = saveSuccess;
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


