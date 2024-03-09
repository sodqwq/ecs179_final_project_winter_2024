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
    public float mJumpForce = 100f;
    bool mJump;
    int numJump;
    private void FixedUpdate()
    {
        bool isGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, m_groundRadius, m_checkLayer);

        //目前check ground 有些问题，回头修复一下，现在先保持为true
        foreach (Collider2D c in colliders)
        {
            isGround = true;
            numJump = 0;
        }

        if(m_anim.GetBool("ground") != isGround)
        {
            m_anim.SetBool("ground", isGround);
        }


        if (m_body.velocity.y == 0)
        {
            m_anim.SetFloat("vSpeed", 0);
        }
        else
        {
            m_anim.SetFloat("vSpeed", m_body.velocity.y / Mathf.Abs(m_body.velocity.y));
        }

        float playerMoving = Input.GetAxis("Horizontal");
        if (numJump == 0)
        {
            mJump = Input.GetButtonDown("Jump");
        }
        else if (numJump == 1)
        {
            if (m_body.velocity.y <= 0)
            {
                mJump = Input.GetButtonDown("Jump");
            }
        }
        Move(playerMoving, mJump);
    }
    private void Move(float playerMoving, bool jump)
    {
        Vector2 move = m_body.velocity;

        if (playerMoving > 0)// h > 0
        {
            move.x = playerMoving / Mathf.Abs(playerMoving) * Speed * Time.deltaTime;
            if (!isFacingRight)
            {
                Flip();
            }
            if (!m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", true);
            }
        }
        else if (playerMoving < 0)
        {
            move.x = playerMoving / Mathf.Abs(playerMoving) * Speed * Time.deltaTime;
            if (isFacingRight)
            {
                Flip();
            }
            if (!m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", true);
            }
        }
        else
        {
            move.x = 0;
            if (m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", false);
            }
        }
        if (jump)
        {
            numJump++;
            move.y = mJumpForce * Time.deltaTime;
        }
        m_body.velocity = move;
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
        mJump = false;
        numJump = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
