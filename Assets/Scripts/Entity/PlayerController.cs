using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControl inputActions;  // 자동 생성된 클래스 이름: PlayerControl
    private Rigidbody2D rb;

    private Vector2 moveInput;           // Move 액션으로부터 입력받을 값
    public float moveSpeed = 5f;

    private void Awake()
    {
        inputActions = new PlayerControl();  // 인스턴스 생성
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        // 방향키 입력 (Vector2) 감지
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        // X축으로만 이동 (2D 플랫폼 스타일)
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }
}