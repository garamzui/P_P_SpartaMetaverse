using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private float moveX;
    private float moveY;

    private SpriteRenderer spriteRenderer; // ��������Ʈ ������

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // �¿� ���⿡ ���� ��������Ʈ ����
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
