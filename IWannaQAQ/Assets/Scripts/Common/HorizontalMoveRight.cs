using UnityEngine;

public class HorizontalMoveRight : MonoBehaviour
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
        // 开始时向左移动
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

        // 只有当计时器到达0，并且碰撞的是"Pit"时才会改变方向
        if ((collision.gameObject.tag == "Pit" && changeDirectionTimer <= 0) || collision.gameObject.tag == "Ground")
        {
            movingLeft = !movingLeft;
            // 立即更新速度，以新方向移动
            rb.velocity = new Vector2(movingLeft ? -speed : speed, rb.velocity.y);
            // 重置计时器
            changeDirectionTimer = changeDirectionCooldown;

            // 更新精灵的翻转状态
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = !movingLeft;
            }
            // Debug.Log($"Direction changed: {(movingLeft ? "Moving Left" : "Moving Right")}, New velocity: {rb.velocity}");
        }
    }
}
