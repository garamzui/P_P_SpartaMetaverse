using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private float moveX;
    private float moveY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 좌우 입력: A/D 또는 ← →
        moveX = Input.GetAxisRaw("Horizontal");

        // 상하 입력: W/S 또는 ↑ ↓
        moveY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // 양 축 모두 이동 (탑다운 이동)
        rb.velocity = new Vector2(moveX, moveY).normalized * moveSpeed;
    }
}




