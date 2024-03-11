using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toGetTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Before Trigger");
      if(other.gameObject.tag == "Player")
        {
            print("trigger");
            Destroy(other.gameObject);
        }      
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
