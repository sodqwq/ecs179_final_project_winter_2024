using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toGetTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider2D other)
    {
      if(other.tag == "Palyer")
        {
            print("trigger");
            Destroy(other);
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
