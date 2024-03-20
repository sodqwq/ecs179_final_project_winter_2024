using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 5f;
    private bool movingUp = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // 添加SpriteRenderer的引用

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取SpriteRenderer组件
    }

    void FixedUpdate()
    {
        // Move Toge0 up or down depending on the direction
        rb.velocity = new Vector2(rb.velocity.x, (movingUp ? 1 : -1) * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Use CompareTag for safer and more efficient tag comparison
        if (collision.gameObject.CompareTag("Ground"))
        {
            movingUp = !movingUp; // Change direction
            rb.velocity = new Vector2(rb.velocity.x, (movingUp ? 1 : -1) * speed); // Apply the new direction immediately
            if (spriteRenderer != null)
            {
                // 翻转Sprite在垂直方向的朝向
                spriteRenderer.flipY = !spriteRenderer.flipY;
            }
        }
    }
}
