using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toGetTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Before Trigger");
      if(other.gameObject.tag == "Player")
        {
            other.SendMessageUpwards("BeDamaged", damage, SendMessageOptions.DontRequireReceiver);
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
