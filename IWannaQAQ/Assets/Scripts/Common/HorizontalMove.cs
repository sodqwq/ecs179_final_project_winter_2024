using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    public float speed = 5f;
    private bool movingLeft = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float changeDirectionCooldown = 0.5f; // 方向改变的冷却时间（秒）
    private float changeDirectionTimer = 0; // 计时器

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void Update()
    {
        // 更新计时器
        if (changeDirectionTimer > 0)
        {
            changeDirectionTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log($"Collided with {collision.gameObject.name}");

        if ((collision.gameObject.tag == "Pit" && changeDirectionTimer <= 0) || collision.gameObject.tag == "Ground")
        {
            movingLeft = !movingLeft;
            // 输出当前方向
            Debug.Log("Moving Left? " + movingLeft);

            // 重新设置速度和计时器
            rb.velocity = new Vector2(movingLeft ? -speed : speed, rb.velocity.y);
            changeDirectionTimer = changeDirectionCooldown;

            // 确认spriteRenderer已经赋值
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = !movingLeft;
                // Debug.Log("Sprite Flipped");
            }
            else
            {
                Debug.LogError("SpriteRenderer not assigned!");
            }
        }
    }

}
