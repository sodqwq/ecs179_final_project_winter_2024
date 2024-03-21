using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Bullet Start");
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Initialize(int direction)
    {
        // Debug.Log("Bullet Initialize");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * direction, 0);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Bullet OnTriggerEnter2D");
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; // Stop the bullet
        rb.isKinematic = true; // Optional: make it kinematic
        rb.simulated = false; // Stops the Rigidbody2D from affecting or being affected by physics

        hitInfo.SendMessage("Beshot", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}