using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCtrl : MonoBehaviour
{
    Transform m_groundCheck;
    public float m_groundCheckRadius = 0.5f;

    Animator m_anim;
    Rigidbody2D m_body;
    // Start is called before the first frame update
    void Start()
    {
        m_groundCheck = transform.Find("GroundCheck");

        m_anim = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
