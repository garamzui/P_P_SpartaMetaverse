using UnityEngine;

/// <summary>
/// �÷��̾��� �ִϸ��̼� ��� ����ϴ� Ŭ����
/// �̵�, ����, ������ ���� �ִϸ��̼� ���¸� �����Ѵ�.
/// ��������Ʈ ���� ��ȯ�� ������ �� �ִ�.
/// </summary>
public class AnimatorController : MonoBehaviour
{
    /// <summary>
    /// �ִϸ��̼� ��Ʈ���� ���� Animator ������Ʈ
    /// </summary>
    private Animator animator;

    /// <summary>
    /// ���� ��ȯ�� ���� SpriteRenderer (�ʿ��� ���)
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Awake�� ���� ������Ʈ�� ������ �� ȣ���
    /// Animator�� SpriteRenderer�� �ʱ�ȭ�Ѵ�.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �̵� �� ���ο� ���� 'IsMove' �Ķ���� ����
    /// </summary>
    /// <param name="isMoving">�����̰� �ִ��� ����</param>
    public void SetMove(bool isMoving)
    {
        animator.SetBool("IsMove", isMoving);
    }

    /// <summary>
    /// ���� �� 'IsJump' Ʈ���� �ߵ�
    /// </summary>
    public void SetJumpTrigger()
    {
        animator.SetTrigger("IsJump");
    }

    /// <summary>
    /// �������� �Ծ��� �� 'IsDamaged' Ʈ���� �ߵ�
    /// </summary>
    public void SetDamageTrigger()
    {
        animator.SetTrigger("IsDamaged");
    }

    /// <summary>
    /// ĳ���� �¿� ���� ������ ó����
    /// </summary>
    /// <param name="flip">�����̸� true, �������̸� false</param>
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