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
        // �¿� �Է�: A/D �Ǵ� �� ��
        moveX = Input.GetAxisRaw("Horizontal");

        // ���� �Է�: W/S �Ǵ� �� ��
        moveY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // �� �� ��� �̵� (ž�ٿ� �̵�)
        rb.velocity = new Vector2(moveX, moveY).normalized * moveSpeed;
    }
}




