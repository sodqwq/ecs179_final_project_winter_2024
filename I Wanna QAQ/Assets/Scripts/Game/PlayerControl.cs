using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float jumpSpeed1;
    public float jumpSpeed2;
    private int jumpCount = 2;
    private Vector3 playerScale;
    private Rigidbody2D playerRigidBody;
    private Animator playerAni;
    private CapsuleCollider2D playerFeet;

    public void InitPlayer()
    {
        playerScale = transform.localScale;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAni = GetComponent<Animator>();
        playerFeet = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Fall();
        IfOnLand();
    }

    private void Run()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = playerScale;
            playerRigidBody.velocity = new Vector2(speed, playerRigidBody.velocity.y);
            playerAni.SetBool("IfRun", true);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
            playerRigidBody.velocity = new Vector2(-speed, playerRigidBody.velocity.y);
            playerAni.SetBool("IfRun", true);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            playerAni.SetBool("IfRun", false);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && jumpCount != 0)
        {
            playerAni.SetBool("IfJump", true);
            playerAni.SetBool("IfFall", false);
            playerAni.SetBool("IfIdle", false);
            if(jumpCount == 2)
            {
                playerRigidBody.velocity = Vector2.up * jumpSpeed1;
                jumpCount--;
            }
            else if (jumpCount == 1)
            {
                playerRigidBody.velocity = Vector2.up * jumpSpeed2;
                jumpCount--;
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            if(playerRigidBody.velocity.y > 3.0f)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 3f);
            }
            // if(playerRigidBody.velocity.y < -8f)    //falling velocity reduce to constant due to air resistance
            // {
            //     playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -8f);
            // }
        }
    }

    private void Fall()
    {
        if(playerRigidBody.velocity.y <= 0f)
        {
            playerAni.SetBool("IfFall", true);
            playerAni.SetBool("IfJump", false);
            playerAni.SetBool("IfIdle", false);
        }
    }

    // If touch ground, reset jump
    // There is probably some bug relate to the composite collider
    // My guess is that there are some issues with the physical shape after composite
    private void IfOnLand()
    {
        Debug.Log(playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")));
        if(playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpCount = 2;
            if(playerAni.GetBool("IfFall")) // Make Sure not Idle when jump
            {
                playerAni.SetBool("IfIdle", true);
                playerAni.SetBool("IfFall", false);
            }
        }
        else
        {
            // If leave ground without jump, jumpCount-- still
            if(jumpCount == 2)
            {
                jumpCount--;
            }
        }
    }
}
