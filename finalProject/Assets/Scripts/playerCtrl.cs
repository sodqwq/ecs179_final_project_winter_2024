using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class playerCtrl : MonoBehaviour
{
    Transform m_groundCheck;
    public float m_groundRadius = 0.5f;

    Animator m_anim;
    Rigidbody2D m_body;
    bool isFacingRight = true;

    public LayerMask m_checkLayer;
    public float Speed = 100f;
    private void FixedUpdate()
    {
        bool isGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, m_groundRadius, m_checkLayer);
        
        //目前check ground 有些问题，回头修复一下，现在先保持为true
        foreach(Collider2D c in colliders)
        {
            if(c.gameObject != gameObject)
            {
                isGround = true;
            }
        }

        if(m_body.velocity.y == 0)
        {
            m_anim.SetFloat("vSpeed",0);
        }
        else
        {
            m_anim.SetFloat("vSpeed", m_body.velocity.y/Mathf.Abs(m_body.velocity.y));
        }

        float playerMoving = Input.GetAxis("Horizontal");
        Move(playerMoving);
    }
    private void Move(float playerMoving)
    {
        Vector2 move = new Vector2(playerMoving* Speed *Time.deltaTime, 0);
        if(playerMoving > 0)
        {
            if(!isFacingRight)
            {
                Flip();
            }
            m_body.velocity = move;
            if(!m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", true);
            }
        }
        else if(playerMoving < 0)
        {
            if(isFacingRight)
            {
                Flip();
            }
            m_body.velocity = move;
            if(!m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", true);
            }
        }
        else{
            
            if(m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", false);
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
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
