using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{

    public Rigidbody2D mTage;
    public float speed = -10f;
    private int turns = 0;
    public int maxTurns = 3;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            turns++;
            Vector2 move = new Vector2(speed, 0);
            mTage.velocity = move * Time.deltaTime;
            if (turns >= maxTurns)
            {
                Destroy(gameObject);
            }
        }
    }
}


