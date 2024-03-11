using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savePiont : MonoBehaviour
{
    public Sprite saveSucess;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Beshot(int k)
    {
        Debug.Log("when shot save point is called");
        PlayerPrefs.SetFloat("playerPositionX", transform.position.x);
        PlayerPrefs.SetFloat("playerPositionY", transform.position.y);
        PlayerPrefs.SetFloat("playerPositionZ", transform.position.z);
        Debug.Log("Player Position Saved");
        spriteRenderer.sprite  = saveSucess;
    }

}
