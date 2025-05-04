using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControl inputActions;  // �ڵ� ������ Ŭ���� �̸�: PlayerControl
    private Rigidbody2D rb;

    private Vector2 moveInput;           // Move �׼����κ��� �Է¹��� ��
    public float moveSpeed = 5f;

    private void Awake()
    {
        inputActions = new PlayerControl();  // �ν��Ͻ� ����
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        // ����Ű �Է� (Vector2) ����
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        // X�����θ� �̵� (2D �÷��� ��Ÿ��)
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }
}