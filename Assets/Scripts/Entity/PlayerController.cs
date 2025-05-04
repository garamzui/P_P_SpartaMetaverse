using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private float moveX;
    private float moveY;

    private SpriteRenderer spriteRenderer; // 스프라이트 반전용

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // 좌우 방향에 따라 스프라이트 반전
        if (moveX != 0)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.flipX = moveX < 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX, moveY).normalized * moveSpeed;
    }



}
