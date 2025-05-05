using UnityEngine;

/// <summary>
/// 플레이어의 애니메이션 제어를 담당하는 클래스
/// 이동, 점프, 데미지 등의 애니메이션 상태를 설정한다.
/// 스프라이트 방향 전환도 포함할 수 있다.
/// </summary>
public class AnimatorController : MonoBehaviour
{
    /// <summary>
    /// 애니메이션 컨트롤을 위한 Animator 컴포넌트
    /// </summary>
    private Animator animator;

    /// <summary>
    /// 방향 전환을 위한 SpriteRenderer (필요할 경우)
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Awake는 게임 오브젝트가 생성될 때 호출됨
    /// Animator와 SpriteRenderer를 초기화한다.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 이동 중 여부에 따라 'IsMove' 파라미터 설정
    /// </summary>
    /// <param name="isMoving">움직이고 있는지 여부</param>
    public void SetMove(bool isMoving)
    {
        animator.SetBool("IsMove", isMoving);
    }

    /// <summary>
    /// 점프 시 'IsJump' 트리거 발동
    /// </summary>
    public void SetJumpTrigger()
    {
        animator.SetTrigger("IsJump");
    }

    /// <summary>
    /// 데미지를 입었을 때 'IsDamaged' 트리거 발동
    /// </summary>
    public void SetDamageTrigger()
    {
        animator.SetTrigger("IsDamaged");
    }

    /// <summary>
    /// 캐릭터 좌우 방향 반전을 처리함
    /// </summary>
    /// <param name="flip">왼쪽이면 true, 오른쪽이면 false</param>
    public void SetFlip(bool flip)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = flip;
    }

    public void SetAttackTrigger()
    {
        animator.SetTrigger("IsAttack");
    }
}