using UnityEngine;

/// <summary>
/// ��� ĳ����(�÷��̾�/���� ��)�� ���� ����� ����ϴ� �߻� ��Ʈ�ѷ�
/// �̵�, ü�� ó��, ��������Ʈ ���� �� �⺻ �ൿ ����
/// </summary>
public abstract class BaseController : MonoBehaviour
{
    [Header("�ɷ�ġ ����")]
    [SerializeField] protected StatusManager status = new StatusManager();  // �ν����Ϳ��� ���̵��� new�� ����

    protected Rigidbody2D rb;                    // �̵��� ���� ������ٵ�
    protected SpriteRenderer spriteRenderer;     // �¿� ������ ���� ��������Ʈ ������

    /// <summary> �ʱ�ȭ: ������ٵ�, ��������Ʈ ã�� ���� �ʱ�ȭ </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //status.Init(this);
    }

    /// <summary> �̵� ó��: �ӵ� ���� �� ���� ���� </summary>
    protected virtual void Move(Vector2 input)
    {
        
            rb.velocity = input.normalized * status.MoveSpeed;
            
            // �¿� ���� ó��
            if (input.x != 0)
                spriteRenderer.flipX = input.x < 0; 

     }
    protected virtual void AutoRun()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = status.MoveSpeed; // x�ุ ������ ����
        rb.velocity = velocity;

        if (velocity.x != 0)
        {
            spriteRenderer.flipX = velocity.x < 0;
        }
    }
    /// <summary> �������� ���� �� �ܺο��� ȣ���ϴ� �޼��� </summary>
    public virtual void TakeDamage(int amount)
    {
        status.TakeDamage(amount);
    }

    // �б� ���� ������ (�ʿ� �� �ܺο��� ���� Ȯ�ο�)
    public int CurrentHP => status.currentHP;
    public int MaxHP => status.MaxHP;
}