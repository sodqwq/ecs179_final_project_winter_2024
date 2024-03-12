using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;

public class playerCtrl : MonoBehaviour
{
    Transform m_groundCheck;
    public float m_groundRadius = 0.5f;

    Animator m_anim;
    Rigidbody2D m_body;
    bool isFacingRight = true;

    [SerializeField]
    private GameObject mBullet;

    public LayerMask m_checkLayer;
    public float Speed = 100f;
    public float mMaxSpeed = 10f;
    public float mJumpForce = 100f;
    bool mIsJumping;
    int mJumpTimes;

    // Max Jump Time
    public float maxJumpTime = 1f;
    // Current Jump Time
    private float mCurrentJumpTime = 0;
    bool mIsGrounded = true;
    Vector2 m_velocity;
    private bool IsGrounded()
    {
        Debug.DrawRay(m_groundCheck.position, Vector2.down * m_groundRadius, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(m_groundCheck.position, Vector2.down, m_groundRadius, m_checkLayer);
        if (hit.collider != null)
        {
            mJumpTimes = 0;
            return true;
        }
        return false;
    }
    private void Update()
    {
        mIsGrounded = IsGrounded();

        if(m_anim.GetBool("ground") != mIsGrounded)
        {
            m_anim.SetBool("ground", mIsGrounded);
        }

        if (mIsJumping && Input.GetButton("Jump"))
        {
            if (mCurrentJumpTime < maxJumpTime)
            {
                m_velocity.x = m_body.velocity.x;
                m_velocity.y = mJumpForce;
                m_body.velocity = m_velocity;
                mCurrentJumpTime += Time.deltaTime;
            }
            else
            {
                mIsJumping = false;
            }
        }
        if (Input.GetButtonDown("Jump") && mJumpTimes < 2)
        {
            mIsJumping = true;
            mCurrentJumpTime = 0;
            mIsGrounded = false;
            m_velocity.x = m_body.velocity.x;
            if (mJumpTimes < 2)
            {
                mJumpTimes++;
            }
        }
        if (mIsJumping && Input.GetButtonUp("Jump"))
        {
            mIsJumping = false;
        }

        float playerMoving = Input.GetAxis("Horizontal");
        Move(playerMoving);

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1");
            GameObject bullet = Instantiate(mBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = isFacingRight ? new Vector2(10, 0) : new Vector2(-10, 0);
        }
    }
    private void Move(float playerMoving)
    {
        Vector2 move = m_body.velocity;

        if (playerMoving != 0)// h > 0
        {
            move.x = m_body.velocity.x + playerMoving / Mathf.Abs(playerMoving) * Speed * Time.deltaTime;
            move.x = Mathf.Clamp(move.x, -mMaxSpeed, mMaxSpeed); // Limit the speed to mMaxSpeed
            if ((move.x > 0 && !isFacingRight) || (move.x < 0 && isFacingRight))
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
        mIsJumping = false;
        mJumpTimes = 0;
    }

    public int HP = 1;
    public GameObject ui_gameover;
    void BeDamaged(int damage)
    {
        Debug.Log("BeDamaged");
        HP -= damage;
        if(HP <= 0)
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
            ui_gameover.SetActive(true);
        }
    }

}
