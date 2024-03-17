using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bullet Start");
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        hitInfo.SendMessage("Beshot", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}