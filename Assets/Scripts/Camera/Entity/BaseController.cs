using UnityEngine;

/// <summary>
/// 모든 캐릭터(플레이어/몬스터 등)의 공통 기능을 담당하는 추상 컨트롤러
/// 이동, 체력 처리, 스프라이트 반전 등 기본 행동 제공
/// </summary>
public abstract class BaseController : MonoBehaviour
{
    [Header("능력치 관리")]
    [SerializeField] protected StatusManager status = new StatusManager();  // 인스펙터에서 보이도록 new로 생성

    protected Rigidbody2D rb;                    // 이동을 위한 리지드바디
    protected SpriteRenderer spriteRenderer;     // 좌우 반전을 위한 스프라이트 렌더러

    /// <summary> 초기화: 리지드바디, 스프라이트 찾고 스탯 초기화 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //status.Init(this);
    }

    /// <summary> 이동 처리: 속도 적용 및 방향 반전 </summary>
    protected virtual void Move(Vector2 input)
    {
        
            rb.velocity = input.normalized * status.MoveSpeed;
            
            // 좌우 반전 처리
            if (input.x != 0)
                spriteRenderer.flipX = input.x < 0; 

     }
    protected virtual void AutoRun()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = status.MoveSpeed; // x축만 강제로 설정
        rb.velocity = velocity;

        if (velocity.x != 0)
        {
            spriteRenderer.flipX = velocity.x < 0;
        }
    }
    /// <summary> 데미지를 받을 때 외부에서 호출하는 메서드 </summary>
    public virtual void TakeDamage(int amount)
    {
        status.TakeDamage(amount);
    }

    // 읽기 전용 접근자 (필요 시 외부에서 상태 확인용)
    public int CurrentHP => status.currentHP;
    public int MaxHP => status.MaxHP;
}