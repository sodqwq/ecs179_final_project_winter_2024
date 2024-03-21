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

    public GameWindow gameWindow;
    bool isFacingRight = true;

    private int jumpCount = 2;
    private Vector3 playerScale;
    private Rigidbody2D playerRigidBody;
    private Animator playerAni;
    private CapsuleCollider2D playerFeet;
    private float debounceTime = 0.5f; // Time to wait before allowing another collision
    private float lastCollisionTime = -1; // Time of the last collision

    private int bulletSpeed = 200;

    [SerializeField]
    private GameObject mBullet;
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

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
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 playerPosition = transform.position;
            int facing = isFacingRight ? 1 : -1;
            Vector3 bulletPosition = new Vector3(playerPosition.x + facing * 20, playerPosition.y, playerPosition.z);
            GameObject bullet = Instantiate(mBullet, bulletPosition, Quaternion.identity);
            bullet.transform.parent = transform.parent;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(facing);
            }

        }
    }

    private void Run()
    {
        Vector2 move = playerRigidBody.velocity;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.localScale = playerScale;
            playerRigidBody.velocity = new Vector2(speed, playerRigidBody.velocity.y);
            playerAni.SetBool("IfRun", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
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
        if ((move.x > 0 && !isFacingRight) || (move.x < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space)) && jumpCount != 0)
        {
            playerAni.SetBool("IfJump", true);
            playerAni.SetBool("IfFall", false);
            playerAni.SetBool("IfIdle", false);
            if (jumpCount == 2)
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
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.Space))
        {
            if (playerRigidBody.velocity.y > 3.0f)
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
        if (playerRigidBody.velocity.y <= 0f)
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
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpCount = 2;
            if (playerAni.GetBool("IfFall")) // Make Sure not Idle when jump
            {
                playerAni.SetBool("IfIdle", true);
                playerAni.SetBool("IfFall", false);
            }
        }
        else
        {
            // If leave ground without jump, jumpCount-- still
            if (jumpCount == 2)
            {
                jumpCount--;
            }
        }
    }

    // DIE!!!!!!
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time - lastCollisionTime < debounceTime) return; // Debounce collisions
        lastCollisionTime = Time.time; // Update the time of the last collision
        // Debug.Log("Player Collision with " + collision.transform.name + " Tag: " + collision.transform.tag);

        if (collision.transform.CompareTag("Trap"))
        {
            gameWindow.GameOver();
        }
        else if (collision.transform.CompareTag("NextLevelSign"))
        {
            gameWindow.NextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Player Trigger with " + other.transform.name + " Tag: " + other.transform.tag);
        if (other.CompareTag("Enemy"))
        {
            gameWindow.GameOver();
        }
    }
}
