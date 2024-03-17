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
        Debug.Log("Bullet Start");
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Bullet OnTriggerEnter2D");
        hitInfo.SendMessage("Beshot", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}