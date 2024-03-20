using UnityEngine;

public class MovingDown: MonoBehaviour
{
    public float speed = 5f;
    private bool movingUp = false; // 改为false，因此一开始会向下移动
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 初始化速度，向下开始移动
        rb.velocity = new Vector2(rb.velocity.x, -speed); // Y方向速度设置为-speed，即向下
    }

    void FixedUpdate()
    {
        // 根据当前方向更新速度
        rb.velocity = new Vector2(rb.velocity.x, (movingUp ? 1 : -1) * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 使用CompareTag来比较标签，更安全、更高效
        if (collision.gameObject.CompareTag("Ground"))
        {
            movingUp = !movingUp; // 改变移动方向
            rb.velocity = new Vector2(rb.velocity.x, (movingUp ? 1 : -1) * speed); // 立即应用新方向的速度

            // 如果有SpriteRenderer，则翻转Sprite
            if (spriteRenderer != null)
            {
                spriteRenderer.flipY = !spriteRenderer.flipY; // 翻转Y轴上的Sprite
            }
        }
    }
}

